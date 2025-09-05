using CertificateGenerator.Worker.Services;
using CertificateManagement.Application.Requests;
using CertificateManagement.Domain.Entities;
using MassTransit;
using MongoDB.Driver;

namespace CertificateGenerator.Worker
{
    public class CertificateGenerationConsumer : IConsumer<CertificateGenerationRequest>
    {
        private readonly IMongoCollection<CertificateEntity> _collection;
        private readonly ILogger<CertificateGenerationRequest> _logger;
        private readonly IHostEnvironment hostEnvironment;
        private readonly CertificateImageGenerator _imageGenerator;

        public CertificateGenerationConsumer(
            IMongoDatabase database,
            ILogger<CertificateGenerationRequest> logger,
            IHostEnvironment hostEnvironment
            
        )
        {
            _collection = database.GetCollection<CertificateEntity>("certificates");
            this._logger = logger;
            this.hostEnvironment = hostEnvironment;
            _imageGenerator = new CertificateImageGenerator();
        }

        public async Task Consume(
            ConsumeContext<CertificateGenerationRequest> context
        )
        {
            var certificateId = context.Message.CertificateId;
            _logger.LogInformation("Recebida solicitação para gerar certificado: {CertificateId}", certificateId);

            var certificate = (await _collection.FindAsync(c => c.Id == certificateId)).FirstOrDefault();
            if (certificate == null)
            {
                _logger.LogError("Certificado com ID {CertificateId} não encontrado.", certificateId);
                return;
            }

            _logger.LogInformation("Gerando imagem para {StudentName}...", certificate.StudentFullName);
            using var imageStream = await _imageGenerator.GenerateImageAsync(
                certificate.StudentFullName,
                string.Join(", ", certificate.Activities),
                certificate.CreatedAt
            );

            var imagesDirectory = Path.Combine(hostEnvironment.ContentRootPath, "Images");

            Directory.CreateDirectory(imagesDirectory);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(imagesDirectory, fileName);

            _logger.LogInformation("Salvando imagem em disco no caminho: {FilePath}", filePath);

           
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageStream.Position = 0; 
                await imageStream.CopyToAsync(fileStream);
            }

            _logger.LogInformation("Imagem salva com sucesso.");


            // Atualiza o MONGODB com a imagem
            var filter = Builders<CertificateEntity>.Filter.Eq(c => c.Id, certificateId);
            var update = Builders<CertificateEntity>.Update.Set(c => c.Image, new CertificateImage
            {
                Path = filePath, 
                FileName = fileName,
                ContentType = "image/png"
            });

            await _collection.UpdateOneAsync(filter, update);
            _logger.LogInformation("--> Documento do certificado {CertificateId} atualizado com o caminho do arquivo local.", certificateId);
        }
    }
}
