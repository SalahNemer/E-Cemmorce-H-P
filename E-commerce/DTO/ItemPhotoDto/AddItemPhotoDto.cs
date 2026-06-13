using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.ItemPhotoDto
{
    public class AddItemPhotoDto
    {
        [Required]
        public List<IFormFile> urlPhoto { get; set; }
        [Required]
        public int itmeId { get; set; }
    }
}
