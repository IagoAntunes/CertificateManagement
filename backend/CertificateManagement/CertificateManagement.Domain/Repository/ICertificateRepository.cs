using CertificateManagement.Domain.Entities;
using System.Runtime.CompilerServices;

namespace CertificateManagement.Domain.Repository
{
    public interface ICertificateRepository
    {
        public Task<CertificateEntity> Create(CertificateEntity certificate);
        public Task<List<CertificateEntity>> GetAll();
    }
}
