using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
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
        public IActionResult Get()
        {
            return Ok(_context.ToDos
                                .Include(toDo => toDo.Category)  //inclui o objeto da categoria (JOIN)
                                .AsNoTracking()  //não rastreia as alterações (melhora a performance)
                                .ToList());      //converte para lista
        }

        [HttpGet("{id}")]
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
        public IActionResult Post([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ToDos.Add(toDo);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = toDo.Id }, toDo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var toDoInDb = _context.ToDos.Find(id);
            if (toDoInDb == null)
            {
                return NotFound();
            }
            toDoInDb.Task = toDo.Task;
            toDoInDb.IsCompleted = toDo.IsCompleted;
            toDoInDb.CreatedAt = toDo.CreatedAt;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
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
        public IActionResult GetCompleted()
        {
            var completedToDos = _context.ToDos.Where(toDo => toDo.IsCompleted);
            return Ok(completedToDos);
        }

        [HttpGet("pending")]
        public IActionResult GetPending()
        {
            var pendingToDos = _context.ToDos.Where(toDo => !toDo.IsCompleted);
            return Ok(pendingToDos);
        }

        [HttpGet("search/{task}")]
        public IActionResult Search(string task)
        {
            var toDos = _context.ToDos.Where(toDo => toDo.Task.Contains(task));
            return Ok(toDos);
        }

        [HttpPut("complete/{id}")]
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