using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("ItemPhoto")]
    public class ItemPhoto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string urlPhoto { get; set; }
        [Required]
        public int itmeId { get; set; }

        [ForeignKey("itmeId")]
        public Item item { get; set; }
    }
}
