using E_commerce.DTO.ItemDto;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Model;

namespace E_commerce.Interface.Reposotiry
{
    public interface IItem
    {
        public Task<List<GetItemDto>> getItemDtoAsync();
        public Task<(Item Item, string Massage)> addItemAsync(AddItemDto item);
        public Task<(bool IsSuccess, string Massage)> deleteItemAsync(int id);
        public Task<(GetItemDto Item, string Massage)> updateItemAsync(UpdateItemDto item, int id);
        public Task<List<GetItemPhotoDto>> getItemPhotoByItemId(int id);
        public Task<List<GetItemSizeDto>> getItemSizeByItemId(int id);
        public Task<GetItemDetailsDto> getItemPhotoOrSizeByItemId(int id);
        public List<GetItemByCategroyDto> getItemByCategroyId(int id);
        public List<GetItemQDto> getItemByChildCategroyId(int id);
    }
}
