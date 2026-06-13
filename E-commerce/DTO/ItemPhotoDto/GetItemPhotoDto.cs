using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.ItemPhotoDto
{
    public class GetItemPhotoDto
    {
        [Key]
        public int id { get; set; }
        public string urlPhoto { get; set; }
        public int itmeId { get; set; }
    }
}
