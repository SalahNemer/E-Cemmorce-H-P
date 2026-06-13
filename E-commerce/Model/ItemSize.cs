using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("ItmeSize")]
    public class ItemSize
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int size { get; set; }
        [Required]
        public decimal price { get; set; }
        //[Required]
        //public string urlFile { get; set; }
        [Required]
        public int itmeId { get; set; }

        [ForeignKey ("itmeId")]
        public Item item { get; set; }

        public ICollection<CartUser> cartUser { get; set; }
        public ICollection<OrderHistory> orderHistory { get; set; }
        public ICollection<FileSize> fileSize { get; set; }


    }
}
