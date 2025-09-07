
using CertificateManagement.Application.Requests;
using CertificateManagement.Core.Utils;
using CertificateManagement.Domain.Dtos;

namespace CertificateManagement.Application.Services.Interfaces
{
    public interface ICertificateService
    {
        public Task<ResultOfT<CertificateDto>> Create(CreateCertificateRequest request);
        public Task<ResultOfT<List<CertificateDto>>> GetBySession(string sessionId);
    }
}
