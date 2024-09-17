using Introduction.Service.Common;
using Introduction.Repository.Common;
using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Introduction.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository repository, IConfiguration config) 
        { 
            _repository=repository;
            _config=config;
        
        }
        public Task<bool> RegisterUser(User user)
        {
            return _repository.RegisterUser(user);
        }

        public Task<User> LoginUser(Login login)
        {
            return _repository.LoginUser(login);
        }

        public Task<bool> UpdateUser(Guid id,User user)
        {
            return _repository.UpdateUser(id, user);
        }

        public async Task<string> CreateToken(TokenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            var id = request.UserID;
            var email = request.Email;

            // Retrieve configuration settings
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

            // Define claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            // Create signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            // Create token handler and generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return serialized token
            return tokenHandler.WriteToken(token);
        }

    }
}
