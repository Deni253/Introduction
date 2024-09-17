using Introduction.Model;
using Introduction.WebAPI.RestModels;
using Microsoft.AspNetCore.Mvc;
using Introduction.Service.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using StackExchange.Redis;

namespace Introduction.WebAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private IAuthService _service;
        private readonly IConfiguration _config;

        public AuthController(IAuthService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Login login) 
        {
            var isSuccessful = await _service.LoginUser(login);

            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password) || isSuccessful==false)
            {
                return BadRequest();
            }
            return Ok(CreateToken(login.token));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var isSuccessful = await _service.RegisterUser(user);

            if (isSuccessful == false)
            {
                return BadRequest();
            }

            //var registerRest = new RegisterRest

            //{
            //    Email = user.Email,
            //    Username = user.Username,
            //};

            return Ok(user);
        }


        //[Authorize("Admin")] // Našem programu nije bitno koji token dobije on će autorizirati svj čak i ako otkomentiram ovu liniju. Kako da mi s obzirom da imamo Role klasu omogućimo ovo samo Adminu a ne i Useru jer ako ćemo raditi to preko Role klase u kodu nama uopće ne treba ovaj authorize admin
        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> Update(Guid id,[FromBody] User user)
        {
            var isSuccessful = await _service.UpdateUser(id,user);

            if (isSuccessful == false)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost("token")]
        public IActionResult CreateToken([FromBody] TokenRequest request)
        {
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest("Username is required.");
                }
                var username = request.Username;

                // Ovo povlači konfiguraciju iz appsettings.json.
                var issuer = _config["JwtSettings:Issuer"];
                var audience = _config["JwtSettings:Audience"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

                //Ovo nam je u biti payload za token. Tu možemo staviti podatke o korisniku
                var claims = new List<Claim>
        {
            //new Claim(JwtRegisteredClaimNames.Sub, UserId), 
            new Claim(JwtRegisteredClaimNames.Name, username), // User Name
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                // Create signing credentials-
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Hash-based Message Authentication Code-HMAC

                // postavljamo claims u Subject i trajanje tokena
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddSeconds(60), // dodajemo kolko vrijedi
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = creds
                };

                // Ovo nam je  token hendler
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Vrati serijalizirani token
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
        }
    }
}
