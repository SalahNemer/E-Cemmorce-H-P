using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.ItemSizeDto
{
    public class AddItemSizeDto
    {
        [Required]
        public int size { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int itmeId { get; set; }
    }
}
