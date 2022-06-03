using Microsoft.AspNetCore.Http;
using Munchkin.Application.Services.Models;

namespace Munchkin.Application.Services.Base
{
    public interface IImageService
    {
        Task<GetObjectReply> GetImageAsync(
            string objectName, CancellationToken cancellationToken);

        Task UploadImagesAsync(
            IEnumerable<IFormFile> files, CancellationToken cancellationToken);
    }
}