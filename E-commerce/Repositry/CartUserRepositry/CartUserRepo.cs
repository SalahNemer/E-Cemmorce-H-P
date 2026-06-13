using DataBase.DBcon;
using E_commerce.DTO.CardDto;
using E_commerce.DTO.CartUserDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Mapper.CardUserMapper;
using E_commerce.Mapper.CartUserMapper;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry.CartUserRepositry
{
    public class CartUserRepo : ICartUser
    {
        private readonly DBC _db;
        public CartUserRepo(DBC db)
        {
            _db = db;
        }

        public async Task<(GetCartUserDto Data, string Massage)> AddCartUserAsync(AddCartUserDto cart)
        {
            var size=await _db.itemsSize.FirstOrDefaultAsync(s=>s.id==cart.itmeSizeId);
            var user = await _db.users.FirstOrDefaultAsync(u => u.id == cart.UserId);
            try
            {
                if (user == null)
                    return (null, "المستخدم غير موجود");
                if (size == null)
                    return (null, "الحجم غير موجود");

                var CART = cart.addCartUserMapper();
                await _db.cartUsers.AddAsync(CART);
                await _db.SaveChangesAsync();
                return (CART.getCartUserMapper(), "done");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> DeleteCartUserAsync(int ID)
        {
            var Id = await _db.cartUsers.FirstOrDefaultAsync(i => i.id == ID);
            try
            {
                if (Id == null)
                    return "not found";
                _db.cartUsers.Remove(Id);
                await _db.SaveChangesAsync();
                return "done";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetCartUserDto>> GetCartUserAsync()
        {
            try
            {
                return await _db.cartUsers.Select(cartUser => cartUser.getCartUserMapper()).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(GetCartUserDto Data, string Massage)> UpdateCartUserAsync(AddCartUserDto cart, int id)
        {
            var user=await _db.users.FirstOrDefaultAsync(u=>u.id==cart.UserId);
            var size=await _db.itemsSize.FirstOrDefaultAsync(s=>s.id==cart.itmeSizeId);
            var itemCart=await _db.cartUsers.FirstOrDefaultAsync(c=>c.id== id);
            try
            {
                if (itemCart == null)
                    return (null, "cart not found");
                if (user == null)
                    return (null, "user not found");
                if (size == null)
                    return (null, "size not found");
                itemCart.itmeSizeId = cart.itmeSizeId;
                itemCart.UserId = cart.UserId;
                await _db.SaveChangesAsync();
                return (itemCart.getCartUserMapper(), "done");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
