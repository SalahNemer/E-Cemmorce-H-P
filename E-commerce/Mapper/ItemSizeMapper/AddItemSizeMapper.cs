using E_commerce.DTO.ItemSizeDto;
using E_commerce.Model;

namespace E_commerce.Mapper.ItemSizeMapper
{
    public static class AddUserMapper
    {
        public static ItemSize addItemSize(this AddItemSizeDto item)
        {
            return new ItemSize
            {
                size = item.size,
                price = item.price,
                itmeId = item.itmeId,
            };
        }
    }
}
