using todo_maui_reposetory.Models;
using Microsoft.Extensions.Logging;

namespace todo_maui_reposetory.Data
{
    public class ProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ILogger<ProjectRepository> logger)
        {
            _logger = logger;
        }

        public async Task<List<Project>> ListAsync()
        {
            // Implementera databaslogik här
            return new List<Project>();
        }

        public async Task SaveItemAsync(Project project)
        {
            // Implementera sparlogik här
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            return Task.FromResult(true);
        }

        internal ReadOnlySpan<Task> DropTableAsync()
        {
            throw new NotImplementedException();
        }

        internal async Task<Project?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        internal async Task DeleteItemAsync(Project project)
        {
            throw new NotImplementedException();
        }

        internal async Task<Project?> GetAsync(object projectID)
        {
            throw new NotImplementedException();
        }
    }
}