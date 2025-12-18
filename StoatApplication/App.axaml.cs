using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api;
using StoatApplication.Core.Logging;

namespace StoatApplication;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        Logger.Initialize(builder => builder
            .AddConsole()
            .AddDebug()
            .SetMinimumLevel(LogLevel.Information)
        );
    }

    // ReSharper disable once AsyncVoidMethod
    /*
     * For whatever reason it kept yelling at me about th AsyncVoidMethod
     * despite I'm already handling with try/catch/finally. So I have to shut up to ReSharper.
     */
    public override void OnFrameworkInitializationCompleted()
    {
        var log = Logger.Create<App>();
        log.LogInformation("OnFrameworkInitializationCompleted starting");

        try
        {
            if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var session = SessionManager.LoadSessionSync();

            if (session != null)
                log.LogInformation("Session restored for user: {User}", session.Name);
            // TODO: Connect websocket
            else
                // TODO: Implement Login Screen since we need it ASAP.
                log.LogWarning("No session found. Launching login screen");

            desktop.MainWindow = new MainWindow();
            log.LogInformation("MainWindow created");
        }
        catch (Exception ex)
        {
            log.LogCritical(ex, "A critial error occured during application startup.");
            // TODO: Add error dialog.
        }
        finally
        {
            base.OnFrameworkInitializationCompleted();
            log.LogInformation("OnFrameworkInitializationCompleted finished");
        }
    }
}