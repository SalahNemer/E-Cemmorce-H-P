using E_commerce.DTO.Login;
using E_commerce.DTO.UserDto;
using loginpage.DBcon;

namespace E_commerce.Interface.Reposotiry
{
    public interface IUser
    {
        public List<GetUserDto> getAllUser();

        public GeneralMsgDto AddUser(AdduserDTO user);

    }
}
