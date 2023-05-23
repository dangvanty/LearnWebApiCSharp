using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebAPITest.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebAPITest.Helpers
{
    // ko dung
    public  class JWT_Handler
    {
        private  readonly AppSettings _appSettings;

        public JWT_Handler(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        public  string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserName", user.UserName),
                    new Claim("Id", user.ID.ToString()),

                    // role
                    new Claim("TokenId", Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
