using E_commerce.DTO.CardDto;
using E_commerce.DTO.CartUserDto;
using E_commerce.DTO.OrderHistory;

namespace E_commerce.Interface.Reposotiry
{
    public interface IOrderHistory
    {
        public Task<List<GetOrderHistoryDto>> GetOrderHistoryAsync();
        public Task<(GetOrderHistoryDto Data, string Massage)> AddOrderHistoryAsync(AddOrderHistoryDto order);
        public Task<string> DeleteOrderHistoryAsync(int id);
        public Task<(GetOrderHistoryDto Data, string Massage)> UpdateOrderHistoryAsync(AddOrderHistoryDto order, int id);

        public Task<bool> UserBoughtItemSize(int userId, int itemSizeId);

    }
}
