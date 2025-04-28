using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using todo_maui_reposetory.Models;
using todo_maui_reposetory.Data;
using todo_maui_reposetory.Services;
using System.Text.Json;

namespace todo_maui_reposetory.PageModels
{
    public partial class MainPageModel : ObservableObject, IProjectTaskPageModel
    {
        private bool _isNavigatedTo;
        private bool _dataLoaded;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ModalErrorHandler _errorHandler;
        private readonly SeedDataService _seedDataService;

        [ObservableProperty]
        private Category _selectedCategory;

        [ObservableProperty]
        private ProjectTask _selectedTask;

        [ObservableProperty]
        private List<CategoryChartData> _todoCategoryData = [];

        [ObservableProperty]
        private List<Brush> _todoCategoryColors = [];

        [ObservableProperty]
        private List<ProjectTask> _tasks = [];

        [ObservableProperty]
        private List<Project> _projects = [];

        [ObservableProperty]
        private ObservableCollection<Category> _categories = new();

        [ObservableProperty]
        private ObservableCollection<TodoList> _todoLists = new();

        [ObservableProperty]
        private TodoList _selectedTodoList;

        [ObservableProperty]
        private string _newListName = string.Empty;

        [ObservableProperty]
        private string _newTaskTitle = string.Empty;

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        private string _today = DateTime.Now.ToString("dddd, MMM d");

        public bool HasCompletedTasks => Tasks?.Any(t => t.IsCompleted) ?? false;

        public MainPageModel(SeedDataService seedDataService, ProjectRepository projectRepository,
            TaskRepository taskRepository, CategoryRepository categoryRepository, ModalErrorHandler errorHandler)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _errorHandler = errorHandler;
            _seedDataService = seedDataService;

            // Initialize collections
            _categories = new ObservableCollection<Category>();
            _todoLists = new ObservableCollection<TodoList>();

            // Load initial data
            Task.Run(async () => await InitData(_seedDataService));
        }

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;

                Projects = await _projectRepository.ListAsync();
                Tasks = await _taskRepository.ListAsync();

                var categories = await _categoryRepository.ListAsync();
                Categories = new ObservableCollection<Category>(categories);

                // Load todo lists from preferences
                var savedListsJson = Preferences.Get("TodoLists", string.Empty);
                if (!string.IsNullOrEmpty(savedListsJson))
                {
                    var savedLists = JsonSerializer.Deserialize<ObservableCollection<TodoList>>(savedListsJson);
                    if (savedLists != null)
                    {
                        TodoLists = savedLists;
                        SelectedTodoList = TodoLists.FirstOrDefault();
                    }
                }

                // Update chart data
                var chartData = new List<CategoryChartData>();
                var chartColors = new List<Brush>();

                foreach (var category in Categories)
                {
                    chartColors.Add(category.ColorBrush);
                    var ps = Projects.Where(p => p.CategoryID == category.ID).ToList();
                    int tasksCount = ps.SelectMany(p => p.Tasks).Count();
                    chartData.Add(new(category.Title, tasksCount));
                }

                TodoCategoryData = chartData;
                TodoCategoryColors = chartColors;
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(HasCompletedTasks));
            }
        }

        private async Task InitData(SeedDataService seedDataService)
        {
            bool isSeeded = Preferences.Default.ContainsKey("is_seeded");

            if (!isSeeded)
            {
                await seedDataService.LoadSeedDataAsync();
            }

            Preferences.Default.Set("is_seeded", true);
            await Refresh();
        }

        [RelayCommand]
        private async Task Refresh()
        {
            try
            {
                IsRefreshing = true;
                await LoadData();
            }
            catch (Exception e)
            {
                _errorHandler.HandleError(e);
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private void NavigatedTo() => _isNavigatedTo = true;

        [RelayCommand]
        private void NavigatedFrom() => _isNavigatedTo = false;

        [RelayCommand]
        private async Task Appearing()
        {
            if (!_dataLoaded)
            {
                await InitData(_seedDataService);
                _dataLoaded = true;
                await Refresh();
            }
            else if (!_isNavigatedTo)
            {
                await Refresh();
            }
        }

        [RelayCommand]
        private Task TaskCompleted(ProjectTask task)
        {
            OnPropertyChanged(nameof(HasCompletedTasks));
            return _taskRepository.SaveItemAsync(task);
        }

        [RelayCommand]
        private async Task AddTodoList()
        {
            if (!string.IsNullOrWhiteSpace(NewListName))
            {
                var newList = new TodoList
                {
                    Name = NewListName,
                    Description = $"Beskrivning för {NewListName}"
                };
                TodoLists.Add(newList);
                NewListName = string.Empty;
                SelectedTodoList = newList;
                SaveTodoLists();
            }
        }

        [RelayCommand]
        private async Task AddTodoTask()
        {
            if (SelectedTodoList != null && !string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                SelectedTodoList.Items.Add(new TodoItem
                {
                    Title = NewTaskTitle,
                    Description = $"Beskrivning för {NewTaskTitle}"
                });
                NewTaskTitle = string.Empty;
                SaveTodoLists();
            }
        }

        [RelayCommand]
        private async Task DeleteTodoList(TodoList list)
        {
            if (list != null)
            {
                TodoLists.Remove(list);
                if (SelectedTodoList == list)
                {
                    SelectedTodoList = TodoLists.FirstOrDefault();
                }
                SaveTodoLists();
            }
        }

        [RelayCommand]
        private async Task DeleteTodoItem(TodoItem item)
        {
            if (SelectedTodoList != null && item != null)
            {
                SelectedTodoList.Items.Remove(item);
                SaveTodoLists();
            }
        }

        private void SaveTodoLists()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(TodoLists, options);
            Preferences.Set("TodoLists", json);
        }

        [RelayCommand]
        private Task AddTask() => Shell.Current.GoToAsync($"task");

        [RelayCommand]
        private Task NavigateToProject(Project project)
            => Shell.Current.GoToAsync($"project?id={project.ID}");

        [RelayCommand]
        private Task NavigateToTask(ProjectTask task)
            => Shell.Current.GoToAsync($"task?id={task.ID}");

        [RelayCommand]
        private async Task CleanTasks()
        {
            var completedTasks = Tasks.Where(t => t.IsCompleted).ToList();
            foreach (var task in completedTasks)
            {
                await _taskRepository.DeleteItemAsync(task);
                Tasks.Remove(task);
            }

            OnPropertyChanged(nameof(HasCompletedTasks));
            Tasks = new(Tasks);
            await AppShell.DisplayToastAsync("All cleaned up!");
        }

        [RelayCommand]
        private async Task AddCategory()
        {
            var newCategory = new Category { Title = "Ny kategori" };
            await _categoryRepository.SaveItemAsync(newCategory);
            Categories.Add(newCategory);
            await Refresh(); // Refresh to update charts
        }
    }
}