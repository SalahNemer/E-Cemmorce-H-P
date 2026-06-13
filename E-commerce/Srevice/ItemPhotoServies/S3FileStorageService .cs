using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using E_commerce.Interface.Serves;

namespace E_commerce.Srevice.ItemPhotoServies
{
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;


        public S3FileStorageService(IConfiguration config)
        {
            _config = config;

            _s3Client = new AmazonS3Client(
                _config["AWS:AccessKey"],
                _config["AWS:SecretKey"],
                RegionEndpoint.GetBySystemName(_config["AWS:Region"])
            );
        }
        public async Task DeleteAsync(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _config["AWS:BucketName"],
                Key = $"items/{fileName}"
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task UpdateAsync(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _config["AWS:BucketName"],
                Key = fileName
            };

            await _s3Client.DeleteObjectAsync(request);
        }
        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var objectKey = $"{folder}/{fileName}";

            var request = new PutObjectRequest
            {
                BucketName = _config["AWS:BucketName"],
                Key = objectKey,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);

            var bucket = _config["AWS:BucketName"];
            var region = _config["AWS:Region"];
            var fileUrl = $"https://{bucket}.s3.{region}.amazonaws.com/{objectKey}";
            return fileUrl;
        }
    }
}
