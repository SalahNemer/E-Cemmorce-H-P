using E_commerce.DTO.CartUserDto;
using E_commerce.Model;

namespace E_commerce.Mapper.CartUserMapper
{
    public static class AddCartUserMapper
    {
        public static CartUser addCartUserMapper(this AddCartUserDto cart)
        {
            return new CartUser
            {
                itmeSizeId = cart.itmeSizeId,
                UserId = cart.UserId,
            };
        }
    }
}
