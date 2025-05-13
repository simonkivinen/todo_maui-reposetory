using Microsoft.Extensions.Logging;
using todo_maui_reposetory.Models;
using todo_maui_reposetory.Data;

namespace todo_maui_reposetory.Pages
{
    public partial class AddEditTodoPage : ContentPage
    {
        private readonly TodoTask _currentTask;
        private readonly TaskRepository _taskRepository;

        public AddEditTodoPage(TodoTask task, TaskRepository taskRepository)
        {
            InitializeComponent();
            _currentTask = task;
            _taskRepository = taskRepository;
            BindingContext = _currentTask;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _taskRepository.SaveItemAsync(_currentTask);
            await Navigation.PopAsync();
        }
    }
}