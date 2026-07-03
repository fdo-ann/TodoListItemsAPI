using Microsoft.Data.SqlClient;
using TodoListItemsAPI.Models;

namespace TodoListItemsAPI.Repositories
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(IConfiguration Configuration)
        {
            _connectionString = Configuration.GetConnectionString("ToDoListDbConnection");
        }

        public async Task SaveTodoItemsAsync(List<TodoItem> todoItems)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            foreach (var item in todoItems)
            {
                string sql = @"If not Exists (select 1 from Todos Where Id =@Id)
                BEGIN
                    Insert into Todos (Id, UserId, Title, Completed)
                    Values (@Id, @UserId, @Title, @Completed)
                END";

                using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@UserId", item.UserId);
                command.Parameters.AddWithValue("@Title", item.Title);
                command.Parameters.AddWithValue("@Completed", item.Completed);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string sql = @"Select * from Todos where Id=@Id";
            using SqlCommand command= new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id",id);

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                return new TodoItem
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Title = reader["Title"].ToString(),
                    Completed = Convert.ToBoolean(reader["Completed"])
                };
            }
            return null;
        }


    }
}
