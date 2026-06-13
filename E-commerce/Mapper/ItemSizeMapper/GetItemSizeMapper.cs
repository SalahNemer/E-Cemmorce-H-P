using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.ItemSizeDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemSizeMapper
{
    public static class GetItemSizeMapper
    {
        public static GetItemSizeDto getItemSize(this ItemSize item)
        {
            return new GetItemSizeDto
            {
               id=item.id,
               size=item.size,
               price=item.price,
               itmeId=item.itmeId,
            };
        }
    }
}
