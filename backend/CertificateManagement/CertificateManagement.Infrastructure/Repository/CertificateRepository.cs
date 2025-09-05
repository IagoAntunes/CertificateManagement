using CertificateManagement.Domain.Entities;
using CertificateManagement.Domain.Repository;
using MongoDB.Driver;

namespace CertificateManagement.Infrastructure.Repository
{
    public class CertificateRepository : ICertificateRepository
    {
        public readonly IMongoCollection<CertificateEntity> _certificatesCollection;

        public CertificateRepository(IMongoDatabase mongoDatabase)
        {
            _certificatesCollection = mongoDatabase.GetCollection<CertificateEntity>("certificates");
        }

        public async Task<CertificateEntity> Create(CertificateEntity certificate)
        {
            await _certificatesCollection.InsertOneAsync(certificate);
            return certificate;
        }

        public async Task<List<CertificateEntity>> GetAll()
        {
            var certificates = await _certificatesCollection.Find(_ => true).ToListAsync();

            return certificates;
        }
    }
}
