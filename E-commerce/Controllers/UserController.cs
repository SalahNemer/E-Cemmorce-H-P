using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.Login;
using E_commerce.Srevice;
using E_commerce.Srevice.UserServies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersService _context;

        public UserController(UsersService context)
        {
            _context = context;
        }


        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] AdduserDTO dto)
        {
            try
            {
                var result = _context.AddUser(dto);

                if (result.ErrorCode == "200")
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى",
                    details = ex.Message
                });
            }
        }
        [HttpGet("GetAllUsers")]

        public IActionResult getAllUser()
        {
            return Ok(_context.getAllUser());
        }

    }
}
