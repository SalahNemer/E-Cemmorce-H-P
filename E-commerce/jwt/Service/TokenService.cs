    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using rafeeqeq.auth.jwt.interfaces;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using E_commerce.DTO.Login;
    using E_commerce.Model;


    namespace rafeeqeq.auth.jwt.Service
    {
        //private readonly 
        public class TokenService : ITokenService
        {
            private readonly IConfiguration _config;
            private readonly SymmetricSecurityKey _key;
            private readonly ILogger<TokenService> _logger;

            public TokenService(IConfiguration config, ILogger<TokenService> logger)
            {
                _config = config;
                _logger = logger;

                var signingKey = _config["JWT:SigningKey"];
                if (string.IsNullOrEmpty(signingKey))
                {
                    _logger.LogError("JWT:SigningKey is not configured.");
                    throw new ArgumentNullException("JWT:SigningKey is missing in configuration.");
                }

                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            }

            public string CreateToken(loginDto accountTbl)
            {
                try
                {
                    var claims = new List<Claim>
                    {
                        new Claim("email", accountTbl.email.ToString()),

                    };

                    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddHours(6),
                        SigningCredentials = creds,
                        Issuer = _config["JWT:Issuer"],
                        Audience = _config["JWT:Audience"]
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return tokenHandler.WriteToken(token);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating the token.");
                    throw;
                }
            }
            public string CreateToken(string userId, string fullName, string email, string urlPhoto, string role , bool isActive)
            {
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),          // user id
                        new Claim(JwtRegisteredClaimNames.Email, email),         // email (standard)
                        new Claim("fullName", fullName ?? ""),                   // custom
                        new Claim("urlPhoto", urlPhoto ?? ""),                   // custom
                        new Claim(ClaimTypes.Role, role ?? "1"),              
                        new Claim("isActive", isActive.ToString())

                    };

                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(6), 
                    SigningCredentials = creds,
                    Issuer = _config["JWT:Issuer"],
                    Audience = _config["JWT:Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

        }
    }
