using CertificateManagement.Application.Requests;
using CertificateManagement.Application.Services.Interfaces;
using CertificateManagement.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService service;

        public CertificateController(ICertificateService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCertificateRequest request)
        {
            var result = await service.Create(request);
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
