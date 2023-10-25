using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Data;
using Api.Data.Dto.ToDoDto;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ToDoController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            return Ok(_context.ToDos
                                .Include(toDo => toDo.Category)  //inclui o objeto da categoria (JOIN)
                                .AsNoTracking()  //não rastreia as alterações (melhora a performance)
                                .ToList());      //converte para lista
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            var toDo = _context.ToDos
                            .Include(toDo => toDo.Category)
                            .Where(toDo => toDo.Id == id)
                            .AsNoTracking()
                            .FirstOrDefault();  //retorna o primeiro ou null
            if (toDo == null)
            {
                return NotFound();
            }
            return Ok(toDo);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post([FromBody] WriteToDoDto toDoDto)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);

            var userId = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (userId == null)
            {
                return Unauthorized();
            }

            var verifyCategory = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == toDoDto.CategoryId && c.UserId == userId.Id);

            if (verifyCategory == null)
            {
                return Unauthorized();
            }

            ToDo toDo = new()
            {
                UserId = userId.Id,
                CategoryId = toDoDto.CategoryId,
                Task = toDoDto.Task,
            };

            _context.ToDos.Add(toDo);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = toDo.Id }, toDo);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, [FromBody] WriteToDoDto toDoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claimId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);

            var userId = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == claimId);

            if (userId == null)
            {
                return Unauthorized();
            }


            var toDoInDb = _context.ToDos.Find(id);

            if (toDoInDb == null)
            {
                return NotFound();
            }

            var verifyCategory = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == toDoDto.CategoryId && c.UserId == userId.Id);

            if (verifyCategory == null)
            {
                return Unauthorized();
            }


            ToDo toDo = new()
            {
                UserId = userId.Id,
                CategoryId = toDoDto.CategoryId,
                Task = toDoDto.Task,
                IsCompleted = toDoDto.IsCompleted,
            };

            toDoInDb.Task = toDo.Task;
            toDoInDb.IsCompleted = toDo.IsCompleted;
            toDoInDb.CreatedAt = toDo.CreatedAt;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            var toDoInDb = _context.ToDos.Find(id);
            if (toDoInDb == null)
            {
                return NotFound();
            }
            _context.ToDos.Remove(toDoInDb);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("completed")]
        [Authorize(Roles = "User")]
        public IActionResult GetCompleted()
        {
            var completedToDos = _context.ToDos.Where(toDo => toDo.IsCompleted);
            return Ok(completedToDos);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "User")]
        public IActionResult GetPending()
        {
            var pendingToDos = _context.ToDos.Where(toDo => !toDo.IsCompleted);
            return Ok(pendingToDos);
        }

        [HttpGet("search/{task}")]
        [Authorize(Roles = "User")]
        public IActionResult Search(String task)
        {
            var toDos = _context.ToDos.Where(toDo => toDo.Task.Contains(task));
            return Ok(toDos);
        }

        [HttpPatch("complete/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Complete(int id)
        {
            var toDoInDb = _context.ToDos.Find(id);
            if (toDoInDb == null)
            {
                return NotFound();
            }
            toDoInDb.IsCompleted = true;
            _context.SaveChanges();
            return NoContent();
        }

    }
}