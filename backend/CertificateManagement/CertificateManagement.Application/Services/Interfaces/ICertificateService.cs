
using CertificateManagement.Application.Requests;
using CertificateManagement.Core.Utils;
using CertificateManagement.Domain.Dtos;

namespace CertificateManagement.Application.Services.Interfaces
{
    public interface ICertificateService
    {
        public Task<ResultBase> Create(CreateCertificateRequest request);
        public Task<ResultOfT<List<CertificateDto>>> GetAll();
    }
}
