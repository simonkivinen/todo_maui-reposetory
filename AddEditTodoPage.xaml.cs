using Microsoft.Extensions.Logging;

namespace todo_maui_reposetory.Pages;

public partial class AddEditTodoPage : ContentPage
{
    private readonly ProjectDetailPageModel currentTask;
    private readonly ProjectDetailPageModel projectModel;

    public AddEditTodoPage(Models.ProjectTask task, ProjectDetailPageModel model)
    {
        InitializeComponent();
        currentTask = new ProjectDetailPageModel(
            new ProjectRepository(new TaskRepository(App.Logger as ILogger<TaskRepository>), new TagRepository(App.Logger as ILogger<TagRepository>), App.Logger as ILogger<ProjectRepository>),
            new TaskRepository(App.Logger),
            new CategoryRepository(App.Logger as ILogger<CategoryRepository>),
            new TagRepository(App.Logger as ILogger<TagRepository>),
            new ModalErrorHandler())
        { 
           
            Task = task,
        };
        projectModel = model;
        BindingContext = currentTask;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!projectModel.Tasks.Contains(currentTask.Task))
        {
            projectModel.Tasks.Add(currentTask.Task);
        }
        await Navigation.PopAsync();
    }
}