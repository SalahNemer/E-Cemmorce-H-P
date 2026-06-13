using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.ChileCategroyDTO
{
    public class AddChileCategroyDTO
    {
        [Required]
        public string childCategoryName { get; set; }
        [Required]
        public int categoryId { get; set; }
    }
}
