namespace E_commerce.Interface.Serves
{
    public interface IFileStorageServicePDF
    {
        Task<string> UploadAsync(IFormFile file, string folder);
        Task DeleteAsync(string fileName);
        Task UpdateAsync(string fileName);

        public string GenerateSignedUrlDownload(string key);

        public string GenerateSignedUrlDownloadAndView(string objectKey);
    }
}
