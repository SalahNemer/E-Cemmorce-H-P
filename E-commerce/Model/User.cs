using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = "";
        public string GoogleId { get; set; } = "";
        public string? PictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int role { get; set; } = 1;
        public string? passwoard { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CartUser> cartUser { get; set; }
        public ICollection<OrderHistory> orderHistory { get; set; }



    }
}
