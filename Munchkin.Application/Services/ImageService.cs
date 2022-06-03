using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Munchkin.Application.Services.Base;
using Munchkin.Application.Services.Models;
using Munchkin.Shared.Options;

namespace Munchkin.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly MinioClient client;
        private readonly MinioOptions options;

        public ImageService(MinioClient client, IOptions<MinioOptions> options)
        {
            this.client = client;
            this.options = options.Value;
        }

        public async Task<GetObjectReply> GetImageAsync(
            string objectName, CancellationToken cancellationToken)
        {
            MemoryStream memoryStream = new();

            var statObjectArgs = new StatObjectArgs()
                .WithBucket(options.BucketName)
                .WithObject(objectName);
            var statObject = await client.StatObjectAsync(statObjectArgs, cancellationToken);

            var objArgs = new GetObjectArgs()
                .WithBucket(options.BucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                });
            await client.GetObjectAsync(objArgs, cancellationToken);

            return new GetObjectReply { Data = memoryStream.ToArray(), ObjectStat = statObject };
        }

        public async Task UploadImagesAsync(IEnumerable<IFormFile> files, CancellationToken cancellationToken)
        {
            foreach (var file in files)
            {
                using var stream = file.OpenReadStream();

                var putObjectArgs = new PutObjectArgs()
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithBucket(options.BucketName)
                    .WithContentType("image/avif")
                    .WithObject($"{Path.GetFileNameWithoutExtension(file.FileName)}.avif");

                await client.PutObjectAsync(putObjectArgs, cancellationToken);
            }
        }
    }
}
