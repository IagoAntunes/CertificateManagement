using CertificateManagement.Application.Requests;
using FluentValidation;

namespace CertificateManagement.API.Validations
{
    public class CreateCertificateRequestValidation : AbstractValidator<CreateCertificateRequest>
    {
        public CreateCertificateRequestValidation()
        {
            RuleFor(x => x.StudentFullName)
                .NotEmpty().WithMessage("Student full name is required.")
                .MaximumLength(100).WithMessage("Student full name must not exceed 100 characters.");

            RuleFor(x => x.Activities)
                .Must(activities => activities != null && activities.Length > 0).WithMessage("Activities cannot be null or empty.");
        }
    }
}
