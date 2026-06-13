using E_commerce.DTO.CardDto;
using E_commerce.DTO.OrderHistory;
using E_commerce.Model;

namespace E_commerce.Mapper.OrderHistoryMapper
{
    public static class GetOrderHistoryMapper
    {
        public static GetOrderHistoryDto getOrderHistoryMapper(this OrderHistory order)
        {
            return new GetOrderHistoryDto
            {
                id = order.id,
                itmeSizeId = order.itemSizeId,
                UserId = order.UserId,
                CreatedAt= order.CreatedAt,
            };
        }
    }
}
