using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTO.Login
{
    public class AdduserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; } 

        public bool IsActive { get; set; } = true;
        public int role { get; set; } = 1;
        [Required]
        public string passwoard { get; set; }
    }
}
