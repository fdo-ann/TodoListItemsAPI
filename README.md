## Steps for Configuration

1. Create a DB

2. Execute following query in MS SQL server use above created DB

    CREATE TABLE Todos
    (
    Id INT PRIMARY KEY,
    UserId INT NOT NULL,
    Title NVARCHAR(500) NOT NULL,
    Completed BIT NOT NULL
    ); 

3. Clone repository
git clone https://github.com/fdo-ann/TodoListItemsAPI.git

4. Update connection string in appsettings.json with your server name DB name user Id and password

    "ConnectionStrings": {
     "ToDoListDbConnection": "Server=SERVER_NAME;Database=DATABASE_NAME;User Id=USER_ID;Password=PASSWORD;Trusted_Connection=True;TrustServerCertificate=True;"
     },

5. Go inside the cloned project folder and execute bellow commands to build and run the project

    dotnet build
  
    dotnet run

    Navigate to URL : https://localhost:YOUR_PORT/swagger

  


## Used Frameworks and Libraries

ADO.NET to communicate directly with the MS SQL server such as executing SQL queries to insert and fetch records

Swagger for documenting the API and to test API easily in the browser
