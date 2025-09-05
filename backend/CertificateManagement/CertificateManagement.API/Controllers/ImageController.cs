using CertificateManagement.API.Attributes;
using CertificateManagement.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageController(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [ApiKey]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return ResultBase.Failure(new Error("no-image","Falha ao gerar imagem", StatusCodes.Status400BadRequest)).ToActionResult();
            }

            var imagesDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
            Directory.CreateDirectory(imagesDirectory);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(imagesDirectory, fileName);

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
          
            var publicUrl = $"{Request.Scheme}://{Request.Host}/Images/{fileName}";
            
            return Ok(new { Url = publicUrl, FileName = fileName });
        }

    }
}
