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

        public Task<bool> UpdateUser(Guid id,User user)
        {
            return _repository.UpdateUser(id, user);
        }

        public async Task<string> LoginUser(string username, string password)
        {
            
            var user = await _repository.GetUserByUsernameAndPassword(username, password);
            if (user == null)
            {
                return null; 
            }
            
            return CreateToken(user);
        }


        public string CreateToken(User user)
        {
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Name, user.Username), 
        new Claim(JwtRegisteredClaimNames.Email, user.Email), 
        new Claim(ClaimTypes.Role, user.Role.Name)
    };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
