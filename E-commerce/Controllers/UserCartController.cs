using DataBase.DBcon;
using E_commerce.DTO.CartUserDto;
using E_commerce.Interface.Reposotiry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        private readonly ICartUser _cartUser;
        public UserCartController(ICartUser cartUser)
        {
            _cartUser = cartUser;
        }

        [HttpGet("getCartUser")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cartUser.GetCartUserAsync());
        }

        [HttpPost("addCartUser")]
        public async Task<IActionResult> add(AddCartUserDto cart)
        {
            var Cart = await _cartUser.AddCartUserAsync(cart);
            if(Cart.Data==null)
                return BadRequest(Cart.Massage);

            return Ok(Cart.Data);
        }

        [HttpDelete("deleteCartUser")]
        public async Task<IActionResult> delete(int id)
        {
            var result=await _cartUser.DeleteCartUserAsync(id);
            if (result == null)
                return BadRequest("not found");
            return Ok(result);
        }

        [HttpPut("updateCartUser")]
        public async Task<IActionResult> update(AddCartUserDto cart, int id)
        {
            var result = await _cartUser.UpdateCartUserAsync(cart, id);
            if(result.Data == null)
            {
                return BadRequest(result.Massage);
            }
            else
            {
                return Ok(new
                {
                    Massage = result.Massage,
                    Item = result.Data
                });
            }

        }
    }
}
