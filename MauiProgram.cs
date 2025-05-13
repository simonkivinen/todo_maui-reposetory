using Microsoft.Extensions.Logging;
using todo_maui_reposetory.Data;
using CommunityToolkit.Maui;

namespace todo_maui_reposetory
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            _ = builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<CategoryRepository>();
            builder.Services.AddSingleton<TaskRepository>();
            builder.Services.AddSingleton<TagRepository>();
            builder.Services.AddLogging(logging =>
            {
                logging.AddDebug();
            });
            return builder.Build();
        }
    }
}