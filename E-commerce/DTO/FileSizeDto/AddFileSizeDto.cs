using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.FileSizeDto
{
    public class AddFileSizeDto
    {
        [Required]
        public List<IFormFile> urlFile { get; set; }
        [Required]
        public int itemSizeId { get; set; }
    }
}
