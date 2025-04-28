using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using CommunityToolkit.Maui;
using todo_maui_reposetory.PageModels;
using todo_maui_reposetory.Pages;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<Tapp>()
            .UseMauiCommunityToolkit()  // Lägg till denna rad
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register ViewModels and Pages
        builder.Services.AddSingleton<MainPageModel>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}