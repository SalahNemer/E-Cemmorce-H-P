using CommunityToolkit.HighPerformance;
using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.DTO.ItemDto;
using E_commerce.DTO.ItemPhotoDto;
using E_commerce.DTO.Login;
using E_commerce.DTO.UserDto;
using E_commerce.Interface.Reposotiry;
using E_commerce.Mapper.UserMapper;
using E_commerce.Mappers.ItemMapper;
using E_commerce.Model;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_commerce.Repositry.UserRepsitry
{
    public class UserRepo : IUser
    {
        private readonly DBC _context;
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public UserRepo(DBC context)
        {
            _context = context;
        }
        public GeneralMsgDto AddUser(AdduserDTO user)
        {
            if (user == null)
                return new GeneralMsgDto(IErrorMsgs.requird_felde, "This fealde is requierd", "400");

            var getUserByEmail = _context.users.FirstOrDefault(p => p.Email == user.Email);
            if (getUserByEmail != null)
                return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "User name already exists", "409");

            user.passwoard = _hasher.HashPassword(null, user.passwoard);

            _context.users.Add(user.AddUsers());
            var result = _context.SaveChanges();

            if (result == 1)
                return new GeneralMsgDto(SuccessfullyMsgs.successflly_operation, "Successfully completed ", "200");

            return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this opration", "400");
        }

        public List<GetUserDto> getAllUser()
        {

            //}
            var users =  _context.users.ToList();
            bool hasUsers = _context.users.Any();
            
            if (hasUsers)
            {
                try
                {
                    return _context.users.Select(Item => Item.getAllUserMapper()).ToList();

                }
                catch (Exception ex) {
                    return null; 
                }
            }
            else
            {
                return null;
            }
        }

}
}
