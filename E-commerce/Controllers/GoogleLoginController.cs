using DataBase.DBcon;
using E_commerce.DTO.Login;
using E_commerce.Model;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rafeeqeq.auth.jwt.interfaces;
using System;

namespace E_commerce.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DBC _context;
        private readonly ITokenService _tokenService;

        public GoogleLoginController(IConfiguration config, DBC context, ITokenService tokenService)
        {
            _config = config;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.IdToken))
                return BadRequest("IdToken is required");

            var googleClientId = _config["Authentication:Google:ClientId"];
            if (string.IsNullOrWhiteSpace(googleClientId))
                return StatusCode(500, "Google ClientId is not configured");

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(
                    request.IdToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { googleClientId }
                    });
            }
            catch
            {
                return Unauthorized("Invalid Google token");
            }

            var email = payload.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized("Google token does not contain email");

            var user = await _context.users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FullName = payload.Name ?? "",
                    GoogleId = payload.Subject,         
                    PictureUrl = payload.Picture,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                user.FullName = payload.Name ?? user.FullName;
                user.PictureUrl = payload.Picture ?? user.PictureUrl;
                user.GoogleId = payload.Subject ?? user.GoogleId;

                await _context.SaveChangesAsync();
            }

            if (!user.IsActive)
                return StatusCode(403, new { message = "الحساب غير مفعل" });

            int roleNumber = Convert.ToInt32(user.role); 



            string token = _tokenService.CreateToken(
                userId: user.id.ToString(),                
                fullName: user.FullName ?? "",
                email: user.Email ?? "",
                urlPhoto: user.PictureUrl ?? "",
                role: user.role.ToString(),                            
                isActive: user.IsActive
            );

            return Ok(new
            {
                message = "Successful Google Login",
                token,
                user = new
                {
                    id = user.id,
                    email = user.Email,
                    fullName = user.FullName,
                    picture = user.PictureUrl,
                    isActive = user.IsActive,
                    role = user.role.ToString(),
                }
            });
        }
    }
}
