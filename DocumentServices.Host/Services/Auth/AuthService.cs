/**
 * namespace DocumentService.Services
{
    using DocumentService.DTOs;
    using DocumentService.Interface;
    using DocumentService.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            // TODO: Replace with real user validation
            if (request.Username != "admin" || request.Password != "123456")
                return null;

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new LoginResponse
            {
                IsSuccess = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
    }

}
*/