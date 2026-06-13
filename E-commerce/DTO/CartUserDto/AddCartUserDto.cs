using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.CartUserDto
{
    public class AddCartUserDto
    {
        [Required]
        public int itmeSizeId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
