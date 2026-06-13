using E_commerce.DTO.Login;
using E_commerce.DTO.UserDto;
using E_commerce.Interface.Reposotiry;
using loginpage.DBcon;

namespace E_commerce.Srevice.UserServies
{
    public class UsersService
    {
        private readonly IUser _context;

        public UsersService(IUser context)
        {
            _context = context;
        }

        public GeneralMsgDto AddUser(AdduserDTO user)
        {
            return _context.AddUser(user);
        }
        public List<GetUserDto> getAllUser()
        {
            return _context.getAllUser();
        }


    }
}
