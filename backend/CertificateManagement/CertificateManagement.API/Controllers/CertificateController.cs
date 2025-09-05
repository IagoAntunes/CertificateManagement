using CertificateManagement.Application.Requests;
using CertificateManagement.Application.Services.Interfaces;
using CertificateManagement.Core.Utils;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService service;
        private readonly IPublishEndpoint publishEndpoint;

        public CertificateController(
            ICertificateService service,
            IPublishEndpoint publishEndpoint
        )
        {
            this.service = service;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCertificateRequest request)
        {
            var result = await service.Create(request);
            if (result.IsSuccess)
            {
                await publishEndpoint.Publish(new CertificateGenerationRequest
                { 
                    CertificateId = result.Value.Id
                });
            }
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAll();

            return result.ToActionResult();

        }

    }
}
