using AutoMapper;
using CertificateManagement.Application.Requests;
using CertificateManagement.Application.Services.Interfaces;
using CertificateManagement.Core.Utils;
using CertificateManagement.Domain.Dtos;
using CertificateManagement.Domain.Entities;
using CertificateManagement.Domain.Repository;

namespace CertificateManagement.Application.Services.Implementation
{
    internal class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository repository;
        private readonly IMapper mapper;

        public CertificateService(
            ICertificateRepository repository,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResultOfT<CertificateDto>> Create(CreateCertificateRequest request)
        {
            var certificateDto = mapper.Map<CertificateDto>(request);
            var certificateEntity = mapper.Map<CertificateEntity>(certificateDto);
            certificateEntity.Id = null;
            certificateEntity.CreatedAt = DateTime.Now;
            var createdCertificateEntity = await repository.Create(certificateEntity);
            var createdCertificateDto = mapper.Map<CertificateDto>(createdCertificateEntity);
            return ResultOfT<CertificateDto>.Success(createdCertificateDto);
        }

        public async Task<ResultOfT<List<CertificateDto>>> GetBySession(string sessionId)
        {
            var certificatesEntity = await repository.GetBySession(sessionId);
            var certificatesDto = mapper.Map<List<CertificateDto>>(certificatesEntity);
            return ResultOfT<List<CertificateDto>>.Success(certificatesDto);
        }
    }
}
