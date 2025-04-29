using Microsoft.Extensions.Logging;

namespace todo_maui_reposetory
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public static ILogger<TaskRepository> Logger { get; internal set; }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}