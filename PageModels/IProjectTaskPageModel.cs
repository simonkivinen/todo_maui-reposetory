using CommunityToolkit.Mvvm.Input;
using todo_maui_reposetory.Models;

namespace todo_maui_reposetory.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}