using Api.Auth;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult Login([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == user.Email);
            
            var passwordEncrypt = _cryptService.Encrypt(user.Password);

            var findPassword = _context.Users.AsNoTracking().FirstOrDefault(u => u.Password == passwordEncrypt);
            
            

            if (findUser == null || findPassword == null)
            {
                return Unauthorized();
            } else
            {
                var token = _authService.Generate(findUser);
                
                return Ok(token);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
 

            var passwordEncrypt = _cryptService.Encrypt(user.Password);

            user.Password = passwordEncrypt;
            
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

    }
}
