using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("CartUser")]
    public class CartUser
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int itmeSizeId { get; set; }

        [ForeignKey("itmeSizeId")]
        public ItemSize itmeSize  { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }



    }
}
