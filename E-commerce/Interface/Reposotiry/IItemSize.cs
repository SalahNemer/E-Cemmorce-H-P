using E_commerce.DTO.ItemDto;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Model;

namespace E_commerce.Interface.Reposotiry
{
    public interface IItemSize
    {
        public Task<GetItemSizeDto> AddItemSizeAsync(AddItemSizeDto itemSize);
        Task<bool> ItemExistsAsync(int itemId);

        public Task<List<GetItemSizeDto>> getItemSize();
        public Task<(bool IsSuccess, string Massage)> deleteItemSizeAsync(int id);

        public Task<(GetItemSizeDto item, string massage)> updateItemSize(UpdateItemSizeDto itemSize, int id);
        public Task<GetItemDto> getItemByItemSizeId(int id);

    }
}
