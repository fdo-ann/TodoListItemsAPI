using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListItemsAPI.Services;

namespace TodoListItemsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("retrieve")]
        public async Task<IActionResult> RetrieveTodos()
        {
            await _todoService.RetrieveAndSaveTodosAsync();

            return Ok(new
            {
                Message = "First 10 todo items retrieved successfully"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItemsByIdAsync(int id) {

            var todoItem = await _todoService.GetTodosByIdAsync(id);
            if (todoItem == null) {
                return NotFound(new {
                    Message = $"Todo Item with ID {id} not found"
                });
            }
            return Ok(todoItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodoItemsAsync(int count) {

            var TodoListItems = await _todoService.GetAllTodoItemsAsync();
            if (TodoListItems.Count==0)
            {
                return NotFound(new
                {
                    Message = "No Todo items found in the table"
                });
            }
            return Ok(TodoListItems);
        
        }
    }
}
