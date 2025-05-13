using todo_maui_reposetory.Models;
using todo_maui_reposetory.Data;
using Microsoft.Maui.Controls;

namespace todo_maui_reposetory.Views
{
    public partial class TaskView : ContentPage
    {
        private readonly TaskRepository _taskRepository;
        private readonly Category _currentCategory;

        public TaskView(TaskRepository taskRepository, Category category)
        {
            InitializeComponent();
            _taskRepository = taskRepository;
            _currentCategory = category;
            Title = category.Title;
            LoadTasks();
        }

        private async Task LoadTasks()
        {
            var tasks = await _taskRepository.ListAsync(_currentCategory.ID);
            TaskList.ItemsSource = tasks;
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Ny uppgift", 
                "Ange uppgiftens namn:", "OK", "Avbryt");

            if (!string.IsNullOrEmpty(result))
            {
                var newTask = new TodoTask 
                { 
                    Title = result,
                    CategoryID = _currentCategory.ID
                };
                await _taskRepository.SaveItemAsync(newTask);
                await LoadTasks();
            }
        }

        private async void OnTaskCompletedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TodoTask task)
            {
                task.IsCompleted = checkBox.IsChecked;
                await _taskRepository.SaveItemAsync(task);
            }
        }

        private async void OnDeleteTaskSwipeClicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var task = swipeItem?.BindingContext as TodoTask;
            
            if (task != null)
            {
                bool answer = await DisplayAlert("Bekräfta", 
                    "Är du säker på att du vill ta bort denna uppgift?", "Ja", "Nej");
                
                if (answer)
                {
                    await _taskRepository.DeleteItemAsync(task);
                    LoadTasks();
                }
            }
        }
    }
}