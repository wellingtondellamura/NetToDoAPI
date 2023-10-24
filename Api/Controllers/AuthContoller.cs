using Api.Auth;
using Api.Data;
using Api.Data.Dto;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthContoller : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly AuthService _authService;
        private readonly CryptService _cryptService;
        public AuthContoller(ApiDbContext context, AuthService authService, CryptService cryptService)
        {
            _context = context;
            _authService = authService;
            _cryptService = cryptService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto userDto)
        {
            User user = new()
            {
                Email = userDto.Email, 
                Password = userDto.Password 
            };

            var findUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == user.Email);           
            var passwordEncrypt = _cryptService.Encrypt(user.Password);
                       

            if (findUser == null || (findUser.Password != passwordEncrypt))
            {
                return Unauthorized();
            } 
            else
            {
                var token = _authService.Generate(findUser);
                return Ok(token);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {

            User user = new()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            var passwordEncrypt = _cryptService.Encrypt(user.Password);
            user.Password = passwordEncrypt;          
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
            
        }

    }
}
