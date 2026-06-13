using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("OrderHistory")]
    public class OrderHistory
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int itemSizeId { get; set; }

        [ForeignKey("itemSizeId")]

        public ItemSize itemSize { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
