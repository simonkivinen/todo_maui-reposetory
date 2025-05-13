using System.Text.Json;
using Microsoft.Extensions.Logging;
using todo_maui_reposetory.Models;
using Microsoft.Maui.Storage;
using todo_maui_reposetory.Data;

namespace todo_maui_reposetory.Data
{
    public class SeedDataService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly TagRepository _tagRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly string _seedDataFilePath = "SeedData.json";
        private readonly ILogger<SeedDataService> _logger;

        public SeedDataService(ProjectRepository projectRepository, TaskRepository taskRepository, TagRepository tagRepository, CategoryRepository categoryRepository, ILogger<SeedDataService> logger)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task LoadSeedDataAsync()
        {
            try 
            {
                await ClearTables();

                await using Stream templateStream = await FileSystem.OpenAppPackageFileAsync(_seedDataFilePath);
                if (templateStream == null)
                {
                    _logger.LogError("Kunde inte öppna seed data filen");
                    return;
                }

                ProjectsJson? payload = null;
                try
                {
                    await using var jsonStream = new MemoryStream();
                    await templateStream.CopyToAsync(jsonStream);
                    jsonStream.Position = 0;
                    
                    payload = await JsonSerializer.DeserializeAsync<ProjectsJson>(jsonStream, JsonContext.Default.ProjectsJson);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Fel vid deserialisering av seed data");
                    return;
                }

                if (payload?.Projects == null)
                {
                    _logger.LogWarning("Inga projekt hittades i seed data");
                    return;
                }

                foreach (var project in payload.Projects)
                {
                    if (project == null) continue;

                    try
                    {
                        if (project.Category != null)
                        {
                            await _categoryRepository.SaveItemAsync(project.Category);
                            project.CategoryID = project.Category.ID;
                        }

                        await _projectRepository.SaveItemAsync(project);

                        if (project.Tasks != null)
                        {
                            foreach (var task in project.Tasks)
                            {
                                task.ProjectID = project.ID;
                                await _taskRepository.SaveItemAsync(task);
                            }
                        }

                        if (project.Tags != null)
                        {
                            foreach (var tag in project.Tags)
                            {
                                await _tagRepository.SaveItemAsync(tag, project.ID);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Fel vid sparande av projekt {project.Title}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Kritiskt fel vid laddning av seed data");
                throw;
            }
        }

        private async Task ClearTables()
        {
            try
            {
                await Task.WhenAll(
                    _projectRepository.DropTableAsync(),
                    _taskRepository.DropTableAsync(),
                    _tagRepository.DropTableAsync(),
                    _categoryRepository.DropTableAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Fel vid rensning av tabeller");
                throw;
            }
        }
    }
}