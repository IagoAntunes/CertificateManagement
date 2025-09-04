using CertificateManagement.Application.Mapping;
using CertificateManagement.Application.Services.Implementation;
using CertificateManagement.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CertificateManagement.Application
{
    public static class AppRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RequestToDtoMapper>();
                cfg.AddProfile<EntityToDtoMapper>();
            });

            services.AddScoped<ICertificateService,CertificateService>();

            return services;
        }
    }
}
