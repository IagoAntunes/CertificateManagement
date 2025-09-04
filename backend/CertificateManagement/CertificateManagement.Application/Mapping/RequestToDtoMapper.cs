using AutoMapper;
using CertificateManagement.Application.Requests;
using CertificateManagement.Domain.Dtos;

namespace CertificateManagement.Application.Mapping
{
    internal class RequestToDtoMapper : Profile
    {
        public RequestToDtoMapper()
        {
            CreateMap<CreateCertificateRequest, CertificateDto>();
        }
    }
}
