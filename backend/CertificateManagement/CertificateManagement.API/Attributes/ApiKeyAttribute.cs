using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CertificateManagement.API.Attributes
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "X-Api-Key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
