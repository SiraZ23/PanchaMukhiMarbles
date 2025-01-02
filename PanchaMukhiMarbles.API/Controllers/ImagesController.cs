using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanchaMukhiMarbles.API.Models.Domain;
using PanchaMukhiMarbles.API.Models.DTO;
using PanchaMukhiMarbles.API.Repository;

namespace PanchaMukhiMarbles.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //Convert DTO To Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    Description = request.FileDescription,
                };

                //Use Repository To Upload Image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupporetd File Extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size Is More Than 10MB. Please Upload A Smaller Size File");
            }
        }
    }
}

                                                      