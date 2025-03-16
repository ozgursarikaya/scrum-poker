using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScrumPoker.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly AuthSettingsDto _authSettings;

        public AuthService(IOptions<AuthSettingsDto> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public async Task<string> GenerateToken(UserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("name", $"{user.Name}")
            };

            var token = new JwtSecurityToken(
                _authSettings.Issuer,
                _authSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_authSettings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
    }
}
