using E_commerce.DTO.Login;
using E_commerce.Model;

namespace rafeeqeq.auth.jwt.interfaces;

public interface ITokenService
{
    public string CreateToken(loginDto accountTbl);

    public string CreateToken(string userId, string fullName, string email, string urlPhoto, string role, bool isActive);
}