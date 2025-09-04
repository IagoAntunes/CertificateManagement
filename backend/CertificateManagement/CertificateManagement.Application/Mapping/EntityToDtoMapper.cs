using AutoMapper;
using CertificateManagement.Domain.Dtos;
using CertificateManagement.Domain.Entities;

namespace CertificateManagement.Application.Mapping
{
    internal class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            // CreateMap<SourceEntity, DestinationDto>();
            CreateMap<CertificateEntity, CertificateDto>().ReverseMap();
        }
    }
}
