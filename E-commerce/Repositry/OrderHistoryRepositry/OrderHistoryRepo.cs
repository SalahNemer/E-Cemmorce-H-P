using DataBase.DBcon;
using E_commerce.DTO.OrderHistory;
using E_commerce.Interface.Reposotiry;
using E_commerce.Mapper.CardUserMapper;
using E_commerce.Mapper.OrderHistoryMapper;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.OrderHistoryRepositry
{
    public class OrderHistoryRepo : IOrderHistory
    {
        private readonly DBC _db;
        public OrderHistoryRepo(DBC db)
        {
            _db = db;
        }

        public async Task<bool> UserBoughtItemSize(int userId, int itemSizeId)
        {
            return await _db.orderHistory
                .AnyAsync(o => o.UserId == userId && o.itemSizeId == itemSizeId);
        }



        public async Task<(GetOrderHistoryDto Data, string Massage)> AddOrderHistoryAsync(AddOrderHistoryDto order)
        {
            var size=await _db.itemsSize.FirstOrDefaultAsync(s=>s.id== order.itmeSizeId);
            var user = await _db.users.FirstOrDefaultAsync(u => u.id == order.UserId);
            try
            {
                if (user == null)
                    return (null, "المستخدم غير موجود");
                if (size == null)
                    return (null, "الحجم غير موجود");

                var CART = order.addOrderHistoryMapper();
                await _db.orderHistory.AddAsync(CART);
                await _db.SaveChangesAsync();
                return (CART.getOrderHistoryMapper(), "done");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> DeleteOrderHistoryAsync(int id)
        {
            var Order=await _db.orderHistory.FirstOrDefaultAsync(o=>o.id==id);
            try
            {
                if (Order == null)
                    return "order not found";
                _db.orderHistory.Remove(Order);
                await _db.SaveChangesAsync();
                return "done";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<GetOrderHistoryDto>> GetOrderHistoryAsync()
        {
            try
            {
                return await _db.orderHistory.Select(orderHistory => orderHistory.getOrderHistoryMapper()).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(GetOrderHistoryDto Data, string Massage)> UpdateOrderHistoryAsync(AddOrderHistoryDto order, int id)
        {
            var user = await _db.users.FirstOrDefaultAsync(u => u.id == order.UserId);
            var size = await _db.itemsSize.FirstOrDefaultAsync(s => s.id == order.itmeSizeId);
            var Order = await _db.orderHistory.FirstOrDefaultAsync(c => c.id == id);
            try
            {
                if (Order == null)
                    return (null, "order not found");
                if (user == null)
                    return (null, "user not found");
                if (size == null)
                    return (null, "size not found");
                Order.itemSizeId = order.itmeSizeId;
                Order.UserId = order.UserId;
                await _db.SaveChangesAsync();
                return (Order.getOrderHistoryMapper(), "done");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
 
        }
    }
}
