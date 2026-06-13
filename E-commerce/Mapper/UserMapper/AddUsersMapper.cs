using E_commerce.DTO.Login;
using E_commerce.Model;

namespace E_commerce.Mapper.UserMapper
{
    public static class AddUsersMapper
    {
        public static User AddUsers (this AdduserDTO user)
        {
            return new User
            {
                Email = user.Email,
                FullName = user.FullName,
                passwoard = user.passwoard,
                IsActive = user.IsActive,
            };
        }
    }
}
