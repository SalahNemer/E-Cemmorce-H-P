using E_commerce.DTO.CardDto;
using E_commerce.Model;

namespace E_commerce.Mapper.CardUserMapper
{
    public static class GetCartUserMapper
    {
        public static GetCartUserDto getCartUserMapper(this CartUser cart)
        {
            return new GetCartUserDto
            {
                id = cart.id,
                itmeSizeId = cart.itmeSizeId,
                UserId = cart.UserId,
            };
        }
    }
}
