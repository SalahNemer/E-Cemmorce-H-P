using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(30)]
        public string categoryName { get; set; }
        public ICollection<Item> item { get; set; }
        public ICollection<ChildCategory> childCategory { get; set; }


    }
}
