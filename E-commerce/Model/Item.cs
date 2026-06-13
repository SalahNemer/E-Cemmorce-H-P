using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(255)]
        public string description { get; set; }
        [Required]
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }

        [Required]
        public int childCategoryId { get; set; }

        [ForeignKey("childCategoryId")]
        public ChildCategory childCategory { get; set; }
        public ICollection<ItemPhoto> itemPhoto { get; set; }
        public ICollection<ItemSize> itemSize { get; set; }


    }
}
