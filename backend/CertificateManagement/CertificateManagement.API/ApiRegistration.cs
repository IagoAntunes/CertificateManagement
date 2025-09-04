using CertificateManagement.API.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CertificateManagement.API
{
    public static class ApiRegistration
    {

        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddFluentValidationAutoValidation()
                    .AddValidatorsFromAssemblyContaining<CreateCertificateRequestValidation>();
            return services;
        }

    }
}
