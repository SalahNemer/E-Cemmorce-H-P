using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.FileSizeDto
{
    public class UpdateFileSizeDto
    {
        [Required]
        public IFormFile? urlFile { get; set; }
        [Required]
        public int itemSizeId { get; set; }
    }
}
