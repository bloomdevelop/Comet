using System;
using Avalonia;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Logging;

namespace StoatApplication;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp();

        var log = Logger.Create<Program>();
        log.LogInformation("Starting StoatApplication");

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        Logger.Initialize(builder => builder
            .AddConsole()
            .AddDebug()
            .SetMinimumLevel(LogLevel.Information)
        );

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}