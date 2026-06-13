using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using E_commerce.Interface.Serves;

namespace E_commerce.Srevice.ItemSizeServies
{
    public class S3FileStorageServicePDF : IFileStorageServicePDF
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;


        public S3FileStorageServicePDF(IConfiguration config)
        {
            _config = config;

            _s3Client = new AmazonS3Client(
                _config["AWS:AccessKey"],
                _config["AWS:SecretKey"],
                RegionEndpoint.GetBySystemName(_config["AWS:Region"])
            );
        }

        public async Task DeleteAsync(string objectKey)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _config["AWS:PDF"],
                Key = objectKey
            };

            await _s3Client.DeleteObjectAsync(request);
        }


        public async Task UpdateAsync(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _config["AWS:PDF"],
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
                BucketName = _config["AWS:PDF"],
                Key = objectKey,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return objectKey;
        }


        //download pdf
        public string GenerateSignedUrlDownload(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "item-pdf",
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(3),
                ResponseHeaderOverrides = new ResponseHeaderOverrides
                {
                    ContentDisposition = "attachment"
                }
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public string GenerateSignedUrlDownloadAndView(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _config["AWS:PDF"],
                Key = objectKey,
                Expires = DateTime.UtcNow.AddMinutes(3),
                ResponseHeaderOverrides = new ResponseHeaderOverrides
                {
                    ContentDisposition = "inline" // ← هذا المهم
                }
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}
