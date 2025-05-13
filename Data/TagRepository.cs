using todo_maui_reposetory.Models;
using Microsoft.Extensions.Logging;

namespace todo_maui_reposetory.Data
{
    public class TagRepository
    {
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(ILogger<TagRepository> logger)
        {
            _logger = logger;
        }

        public async Task<List<Tag>> ListAsync()
        {
            // Implementera databaslogik här
            return new List<Tag>();
        }

        public async Task SaveItemAsync(Tag tag, int projectId)
        {
            // Implementera sparlogik här med projektID
        }

        public async Task DeleteItemAsync(int id)
        {
            // Implementera borttagningslogik här
        }

        public Task DropTableAsync()
        {
            // Implementera logik för att rensa tabellen
            return Task.CompletedTask;
        }

        internal async Task SaveItemAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        internal async Task DeleteItemAsync(Tag tag, int iD)
        {
            throw new NotImplementedException();
        }
    }
}