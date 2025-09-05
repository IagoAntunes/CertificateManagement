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

        public CertificateGenerationConsumer(
            IMongoDatabase database,
            ILogger<CertificateGenerationRequest> logger
            
        )
        {
            _collection = database.GetCollection<CertificateEntity>("certificates");
            this._logger = logger;
        }

        public async Task Consume(
            ConsumeContext<CertificateGenerationRequest> context
        )
        {
            var certificateId = context.Message.CertificateId;
            _logger.LogInformation("Recebida solicitação para gerar certificado: {CertificateId}", certificateId);

            _logger.LogInformation("Gerando imagem...");
            await Task.Delay(5000);
            var imageUrl = $"https://storage.example.com/images/{Guid.NewGuid()}.png";
            _logger.LogInformation("Imagem gerada e salva em: {ImageUrl}", imageUrl);

            var filter = Builders<CertificateEntity>.Filter.Eq(c => c.Id, certificateId);
            var update = Builders<CertificateEntity>.Update.Set(c => c.Image, new CertificateImage
            {
                Path = imageUrl,
                FileName = Path.GetFileName(imageUrl),
                ContentType = "image/png"
            });

            await _collection.UpdateOneAsync(filter, update);
            _logger.LogInformation("Documento do certificado {CertificateId} atualizado com a imagem.", certificateId);
        }
    }
}
