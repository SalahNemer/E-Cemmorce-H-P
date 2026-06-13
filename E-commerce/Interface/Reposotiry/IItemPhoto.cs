using E_commerce.DTO.ItemPhotoDto;
using E_commerce.Model;

namespace E_commerce.Interface.Reposotiry
{
    public interface IItemPhoto
    {
        Task AddAsync(ItemPhoto itemPhoto);
        Task<bool> ItemExistsAsync(int itemId);

        public Task<List<GetItemPhotoDto>> getItemPhoto();
        public Task<(bool IsSuccess, string Massage)> deleteItemAsync(int id);

        public Task<(GetItemPhotoDto item,string massage)> updateItem(UpdateItemPhotoDto itemPhoto,int id);
    }
}
