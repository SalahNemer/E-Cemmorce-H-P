using E_commerce.DTO.CartUserDto;
using E_commerce.DTO.OrderHistory;
using E_commerce.Model;

namespace E_commerce.Mapper.OrderHistoryMapper
{
    public static class AddOrderHistoryMapper
    {
        public static OrderHistory addOrderHistoryMapper(this AddOrderHistoryDto order)
        {
            return new OrderHistory
            {
                itemSizeId = order.itmeSizeId,
                UserId = order.UserId,
            };
        }
    }
}
