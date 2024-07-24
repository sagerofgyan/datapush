using datacapture.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace datacapture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configure;


        private readonly Datacontext _context;

        public LoginController(IConfiguration configuration, Datacontext context)
        {
            _configure = configuration;
            _context = context;
        }

        private User AuthenticateUser(User user)
        {
           User _user=new User();

           var v= _context.Users.Select(a => a.username == user.username && a.password == user.password).ToList();

            if (!v.Equals(null))
            {
                _user.username = user.username;
            }
            return _user;
        }
        private string GenrateToken(User user)
        {
            var clamis = new[] { new Claim(ClaimTypes.NameIdentifier, user.username),
           new Claim(ClaimTypes.Role,"Admin")
           };


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["JWTConfig:Key"]));
            var credenitials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configure["JWTConfig:Issur"], _configure["JWTConfig:Audience"], clamis
                , expires: DateTime.Now.AddMinutes(1)
                , signingCredentials: credenitials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {

            IActionResult responce = Unauthorized();
            var _user = AuthenticateUser(user);
            if (_user != null)
            {
                var token = GenrateToken(_user);
                responce = Ok(new { token = token });
            }
            return responce;
        }





    }
}
