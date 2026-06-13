using E_commerce.DTO.ItemDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemMapper
{
    public static class UpdateItemMapper
    {
        public static Item updateItem(this UpdateItemDto item)
        {
            return new Item
            {
                title = item.title,
                description = item.description,
                categoryId = item.categoryId,
                childCategoryId = item.childCategoryId,
            };
        }
    }
}
