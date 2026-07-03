using TodoListItemsAPI.Models;
using TodoListItemsAPI.Repositories;

namespace TodoListItemsAPI.Services
{
    public class TodoService
    {
        private readonly HttpClient _httpClient;
        private readonly TodoRepository _todoRepository;
        public TodoService(HttpClient httpClient, TodoRepository todoRepository)
        {
            _httpClient = httpClient;
            _todoRepository = todoRepository;
        }

        public async Task RetrieveAndSaveTodosAsync()
        {
            var todoListResponse = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");

            var todoList = await todoListResponse.Content.ReadFromJsonAsync<List<TodoItem>>();

            var firstTenTodoItems = todoList.Take(10).ToList();

            await _todoRepository.SaveTodoItemsAsync(firstTenTodoItems);

        }

        public async Task<TodoItem?> GetTodosByIdAsync(int id)
        {
            List<TodoItem> todos = new List<TodoItem>(1);

            // 1. Check whether record exist in DB
            var todoItem = await _todoRepository.GetTodoItemByIdAsync(id);

            if(todoItem != null)
            {
                return todoItem;
            }
            //2. Retrieve from API

            var todoListResponse = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
            if (todoListResponse == null) { 
                return null;
            }
            todoItem = await todoListResponse.Content.ReadFromJsonAsync<TodoItem>();

            if(todoItem != null)
            {
                todos.Add(todoItem);
                //3.Save TodoItem
                await _todoRepository.SaveTodoItemsAsync(todos);
            }
            return todoItem;
        }

        public async Task<List<TodoItem?>> GetAllTodoItemsAsync()
        {
            var TodoItemsList = await _todoRepository.GetAllTodoItemAsync();
            return TodoItemsList;
        }
    }
}
