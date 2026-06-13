namespace E_commerce.DTO.UserDto
{
    public class GetUserDto
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? PictureUrl { get; set; }
        public bool IsActive { get; set; }
        public int role { get; set; }
    }
}
