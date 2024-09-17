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
using Introduction.Service;
using Microsoft.AspNetCore.Identity.Data;

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
        public async Task<IActionResult> Login(Login request)
        {
            var token = await _service.LoginUser(request.Username, request.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
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

            return Ok(user);
        }

        //[Authorize("Admin")] // Našem programu nije bitno koji token dobije on će autorizirati svj čak i ako otkomentiram ovu liniju. Kako da mi s obzirom da imamo Role klasu omogućimo ovo samo Adminu a ne i Useru jer ako ćemo raditi to preko Role klase u kodu nama uopće ne treba ovaj authorize admin
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] User user)
        {
            //var currentUserId = GetCurrentUserId();
            //var currentUser = await _userService.GetUserById(currentUserId);

            //bool isAdmin = currentUser.UserRoles.Any(ur => ur.Role.Name == "Admin");

            //if (!isAdmin)
            //{
            //    return Forbid();
            //}

            var isSuccessful = await _service.UpdateUser(id, user);

            if (isSuccessful == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}