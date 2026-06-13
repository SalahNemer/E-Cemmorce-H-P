using E_commerce.DTO.Login;
using E_commerce.DTO.UserDto;
using E_commerce.Model;

namespace E_commerce.Mapper.UserMapper
{
    public static class getUserMapper
    {
        public static GetUserDto getAllUserMapper(this User user)
        {
            return new GetUserDto
            {
                id = user.id ,
                Email = user.Email,
                FullName = user.FullName,
                PictureUrl = user.PictureUrl,
                IsActive = user.IsActive,
                role = user.role
            };
        }
    }
}
