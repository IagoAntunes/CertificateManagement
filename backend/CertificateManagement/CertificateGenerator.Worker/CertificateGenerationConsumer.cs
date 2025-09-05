using CertificateGenerator.Worker.Services;
using CertificateManagement.Application.Requests;
using CertificateManagement.Domain.Entities;
using MassTransit;
using MongoDB.Driver;
using System.Net.Http;
using System.Net.Http.Json;

namespace CertificateGenerator.Worker
{
    public class CertificateGenerationConsumer : IConsumer<CertificateGenerationRequest>
    {
        private readonly IMongoCollection<CertificateEntity> _collection;
        private readonly ILogger<CertificateGenerationRequest> _logger;
        private readonly IHostEnvironment hostEnvironment;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly CertificateImageGenerator _imageGenerator;

        public CertificateGenerationConsumer(
            IMongoDatabase database,
            ILogger<CertificateGenerationRequest> logger,
            IHostEnvironment hostEnvironment,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration

        )
        {
            _collection = database.GetCollection<CertificateEntity>("certificates");
            this._logger = logger;
            this.hostEnvironment = hostEnvironment;
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
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

            _logger.LogInformation("Enviando imagem para a API...");
            var client = _httpClientFactory.CreateClient();

            var apiKey = _configuration.GetValue<string>("ApiKey");
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            imageStream.Position = 0;

            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(imageStream), "file", $"{Guid.NewGuid()}.png");

            var baseUrl = _configuration.GetValue<string>("ApiSettings:BaseUrl");
            var apiUrl = $"{baseUrl}/api/image";

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Falha ao fazer upload da imagem para a API. Status: {StatusCode}", response.StatusCode);
                return;
            }

            var uploadResult = await response.Content.ReadFromJsonAsync<ImageUploadResponse>();
            var publicUrl = uploadResult.Url;
            var fileName = uploadResult.FileName;

            // Atualiza o MONGODB com a imagem
            var filter = Builders<CertificateEntity>.Filter.Eq(c => c.Id, certificateId);
            var update = Builders<CertificateEntity>.Update.Set(c => c.Image, new CertificateImage
            {
                Path = publicUrl, 
                FileName = fileName,
                ContentType = "image/png"
            });

            await _collection.UpdateOneAsync(filter, update);
            _logger.LogInformation("--> Documento do certificado {CertificateId} atualizado com o caminho do arquivo local.", certificateId);
        }
    }
    public class ImageUploadResponse
    {
        public string Url { get; set; }
        public string FileName { get; set; }
    }

}
