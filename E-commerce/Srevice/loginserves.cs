using DataBase.DBcon;
using E_commerce.DTO.Login;
using Microsoft.AspNetCore.Identity;
using rafeeqeq.auth.jwt.interfaces;
using V1.jwt;

namespace V1.Service
{
    public class loginserves
    {
        private readonly DBC _context;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public loginserves(DBC con, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = con;
        }

        public LoginResponse login(loginDto login)
        {
            var user = _context.users.FirstOrDefault(p => p.Email == login.email);

            if (user == null)
                return new LoginResponse("Invalid Your Username or Password", "", 0, 0);

            if (!user.IsActive)
                return new LoginResponse("الحساب غير مفعل", "", 0, 0);

            // ✅ Verify hashed password (بدل ==)
            var verify = _hasher.VerifyHashedPassword(null, user.passwoard, login.Password);

            if (verify == PasswordVerificationResult.Failed)
                return new LoginResponse("Invalid Your Username or Password", "", 0, 0);

            int roleNumber = Convert.ToInt32(user.role);
            string roleAsString = roleNumber.ToString();

            string token = _tokenService.CreateToken(
                userId: user.id.ToString(),
                fullName: user.FullName ?? "",
                email: user.Email ?? "",
                urlPhoto: user.PictureUrl ?? "",
                role: roleAsString,
                isActive: user.IsActive
            );

            int accountStatus = user.IsActive ? 1 : 0;

            return new LoginResponse("Successful Login", token, roleNumber, accountStatus);
        }
    }
}