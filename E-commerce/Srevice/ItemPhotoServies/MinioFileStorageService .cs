using E_commerce.Interface.Serves;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace E_commerce.Srevice.ItemPhotoServies
{
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly IMinioClient _minio;
        private readonly IConfiguration _config;
        public MinioFileStorageService(IMinioClient minio, IConfiguration config)
        {
            _minio = minio;
            _config = config;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var objectName = $"{folder}/{fileName}";
            var bucket = _config["MinIO:Bucket"];

            using var stream = file.OpenReadStream();

            await _minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(file.ContentType));

            return fileName;
        }

        public async Task DeleteAsync(string fileName)
        {
            var bucket = _config["MinIO:Bucket"];

            await _minio.RemoveObjectAsync(
                new RemoveObjectArgs()
                    .WithBucket(bucket)
                    .WithObject($"items/{fileName}")
            );
        }

        public Task UpdateAsync(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
