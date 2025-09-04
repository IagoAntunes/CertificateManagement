using Microsoft.AspNetCore.Http;

namespace CertificateManagement.Core.Utils
{
    public readonly record struct Error(string Code, string Description, int StatusCode = StatusCodes.Status400BadRequest)
    {
        public static readonly Error None = new("", "", StatusCodes.Status200OK);
    }
}
