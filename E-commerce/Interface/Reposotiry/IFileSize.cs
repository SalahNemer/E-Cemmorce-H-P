using E_commerce.DTO.FileSizeDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Model;

namespace E_commerce.Interface.Reposotiry
{
    public interface IFileSize
    {
        public Task<List<GetFileSizeDto>> GetFileSize();
        public Task AddFileSize(FileSize fileSize);
        Task<bool> ItemExistsAsync(int itemId);
        public Task<(bool IsSuccess, string Massage)> deleteFileSizeAsync(int id);
        public Task<(GetFileSizeDto item, string massage)> updateFileSize(UpdateFileSizeDto FileSizeSize, int id);



    }
}
