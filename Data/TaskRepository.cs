using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using todo_maui_reposetory.Models;

namespace todo_maui_reposetory.Data
{
    public class TaskRepository
    {
        private bool _hasBeenInitialized = false;
        private readonly ILogger _logger;

        public TaskRepository(ILogger<TaskRepository> logger)
        {
            _logger = logger;
        }

        private async Task Init()
        {
            if (_hasBeenInitialized)
                return;

            await using var connection = new SqliteConnection(Constants.DatabasePath);
            await connection.OpenAsync();

            try
            {
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Task (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    IsCompleted INTEGER NOT NULL,
                    CategoryID INTEGER NOT NULL,
                    DueDate TEXT,
                    CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP
                );";
                await createTableCmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating Task table");
                throw;
            }

            _hasBeenInitialized = true;
        }

        public async Task<List<TodoTask>> ListAsync(int categoryId)
        {
            await Init();
            await using var connection = new SqliteConnection(Constants.DatabasePath);
            await connection.OpenAsync();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Task WHERE CategoryID = @categoryId ORDER BY DueDate, CreatedDate";
            selectCmd.Parameters.AddWithValue("@categoryId", categoryId);
            var tasks = new List<TodoTask>();

            await using var reader = await selectCmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tasks.Add(new TodoTask
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    IsCompleted = reader.GetBoolean(3),
                    CategoryID = reader.GetInt32(4),
                    DueDate = reader.IsDBNull(5) ? null : reader.GetString(5)
                });
            }

            return tasks;
        }

        public async Task<TodoTask?> GetAsync(int id)
        {
            await Init();
            await using var connection = new SqliteConnection(Constants.DatabasePath);
            await connection.OpenAsync();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Task WHERE ID = @id";
            selectCmd.Parameters.AddWithValue("@id", id);

            await using var reader = await selectCmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new TodoTask
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    IsCompleted = reader.GetBoolean(3),
                    CategoryID = reader.GetInt32(4),
                    DueDate = reader.IsDBNull(5) ? null : reader.GetString(5)
                };
            }

            return null;
        }

        public async Task<int> SaveItemAsync(TodoTask item)
        {
            await Init();
            await using var connection = new SqliteConnection(Constants.DatabasePath);
            await connection.OpenAsync();

            var saveCmd = connection.CreateCommand();
            if (item.ID == 0)
            {
                saveCmd.CommandText = @"
                INSERT INTO Task (Title, Description, IsCompleted, CategoryID, DueDate)
                VALUES (@Title, @Description, @IsCompleted, @CategoryID, @DueDate);
                SELECT last_insert_rowid();";
            }
            else
            {
                saveCmd.CommandText = @"
                UPDATE Task 
                SET Title = @Title, Description = @Description, 
                    IsCompleted = @IsCompleted, CategoryID = @CategoryID, 
                    DueDate = @DueDate
                WHERE ID = @ID";
                saveCmd.Parameters.AddWithValue("@ID", item.ID);
            }

            saveCmd.Parameters.AddWithValue("@Title", item.Title);
            saveCmd.Parameters.AddWithValue("@Description", item.Description);
            saveCmd.Parameters.AddWithValue("@IsCompleted", item.IsCompleted);
            saveCmd.Parameters.AddWithValue("@CategoryID", item.CategoryID);
            saveCmd.Parameters.AddWithValue("@DueDate", item.DueDate as object ?? DBNull.Value);

            var result = await saveCmd.ExecuteScalarAsync();
            if (item.ID == 0)
            {
                item.ID = Convert.ToInt32(result);
            }

            return item.ID;
        }

        public async Task<int> DeleteItemAsync(TodoTask item)
        {
            await Init();
            await using var connection = new SqliteConnection(Constants.DatabasePath);
            await connection.OpenAsync();

            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM Task WHERE ID = @id";
            deleteCmd.Parameters.AddWithValue("@id", item.ID);

            return await deleteCmd.ExecuteNonQueryAsync();
        }

        internal object DropTableAsync()
        {
            throw new NotImplementedException();
        }

        internal async Task<List<ProjectTask>> ListAsync()
        {
            throw new NotImplementedException();
        }

        internal async Task SaveItemAsync(ProjectTask task)
        {
            throw new NotImplementedException();
        }

        internal async Task DeleteItemAsync(ProjectTask task)
        {
            throw new NotImplementedException();
        }
    }
}