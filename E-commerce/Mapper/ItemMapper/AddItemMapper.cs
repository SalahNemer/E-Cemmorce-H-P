using E_commerce.DTO.ItemDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemMapper
{
    public static class AddItemMapper
    {
        public static Item addItem(this AddItemDto addDto)
        {
            return new Item
            {
                title = addDto.title,
                description = addDto.description,
                categoryId = addDto.categoryId,
                childCategoryId = addDto.childCategoryId,
            };
        }
    }
}
