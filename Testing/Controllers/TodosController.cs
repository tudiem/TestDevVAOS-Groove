using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testing.Models;

namespace Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodosController(TodoContext context) {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<TodoItemDTO> GetAll()
        {
            var todos = _context.GetAll();
            return todos.Select(m => m.ToDTO());
        }

        [HttpPost]
        public TodoItemDTO? Add(TodoItemDTO todoDTO)
        {
            var todoItemCreated = _context.Add(todoDTO.ToEntity());
            return todoItemCreated != null ? todoItemCreated.ToDTO() : null;
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItemDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }
            var result = _context.Update(todoDTO.ToEntity());

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = _context.Delete(id);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
