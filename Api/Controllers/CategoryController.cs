using System.Security.Claims;
using Api.Data;
using Api.Data.Dto.CategoryDto;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CategoryController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            //Get user id from token 
            var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid);
            if (userId == null)
            {
                return Unauthorized();
            }
            //check if user exist in database
            var user = _context.Users.Find(userId.Value);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(_context.Categories.Where(u => u.UserId == user.Id).ToList());          
        }
            
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post([FromBody] WriteCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Identity.Name == null)
            {
                return Unauthorized();
            }

            var userId = _context.Users.AsNoTracking().FirstOrDefault(u => u.Name == User.Identity.Name);

            if (userId == null)
            {
                return Unauthorized();
            }

            Category category = new()
            {
                Name = categoryDto.Name,
                Color = categoryDto.Color,
                UserId = userId.Id,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, [FromBody] WriteCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category()
            {
                Name = categoryDto.Name,
                Color = categoryDto.Color,
            };

            var categoryInDb = _context.Categories.Find(id);

            if (categoryInDb == null)
            {
                return NotFound();
            }

            categoryInDb.Name = category.Name;
            categoryInDb.Color = category.Color;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return NoContent();
        }

        
    }
}