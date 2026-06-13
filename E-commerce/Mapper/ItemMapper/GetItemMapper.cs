using E_commerce.DTO.ItemDto;
using E_commerce.Model;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace E_commerce.Mappers.ItemMapper
{
    public static class GetItemMapper
    {
        public static GetItemDto getItem(this Item item)
        {
            return new GetItemDto
            {
                Id = item.Id,
                title=item.title,
                description=item.description,
                categoryId=item.categoryId,
                childCategoryId=item.childCategoryId,
            };
        }
    }
}
