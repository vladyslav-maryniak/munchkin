using Microsoft.AspNetCore.Mvc;
using Munchkin.Application.Services.Base;

namespace Munchkin.API.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageAsync(
            string id, CancellationToken cancellationToken)
        {
            var reply = await imageService.GetImageAsync(id, cancellationToken);

            if (reply is null || reply.Data is null || reply.ObjectStat is null)
            {
                return NotFound();
            }

            return File(reply.Data, reply.ObjectStat.ContentType);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImagesAsync(
            [FromForm] IEnumerable<IFormFile> files, CancellationToken cancellationToken)
        {
            await imageService.UploadImagesAsync(files, cancellationToken);

            return Ok();
        }
    }
}
