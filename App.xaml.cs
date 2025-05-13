
using Microsoft.Extensions.Logging;
using todo_maui_reposetory.Views;
using todo_maui_reposetory.Data;

namespace todo_maui_reposetory
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public static ILogger<TaskRepository> Logger { get; set; } = default!;

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}