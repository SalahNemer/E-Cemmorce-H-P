using E_commerce.DTO.CardDto;
using E_commerce.DTO.CartUserDto;

namespace E_commerce.Interface.Reposotiry
{
    public interface ICartUser
    {
        public  Task<List<GetCartUserDto>> GetCartUserAsync();
        public  Task<(GetCartUserDto Data, string Massage)> AddCartUserAsync(AddCartUserDto cart);
        public Task<string> DeleteCartUserAsync(int id);
        public Task<(GetCartUserDto Data, string Massage)> UpdateCartUserAsync(AddCartUserDto cart,int id);

    }
}
