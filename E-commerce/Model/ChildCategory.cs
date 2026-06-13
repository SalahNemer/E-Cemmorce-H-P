using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("ChildCategory")]
    public class ChildCategory
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string childCategoryName { get; set; }
        [Required]
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }

        public ICollection<Item> item { get; set; }


    }
}
