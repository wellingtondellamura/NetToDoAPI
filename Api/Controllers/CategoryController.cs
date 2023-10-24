using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_context.Categories);
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
        public IActionResult Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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