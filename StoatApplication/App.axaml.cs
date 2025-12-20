using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api;
using StoatApplication.Core.Logging;
using StoatApplication.Core.WebSocket;
using StoatApplication.Views;

namespace StoatApplication;

public class App : Application
{
    public static WebSocketClient? WebSocket { get; set; }

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

            desktop.Exit += async (_, _) =>
            {
                if (WebSocket != null)
                {
                    await WebSocket.DisposeAsync();
                    WebSocket = null;
                }
            };

            var session = SessionManager.LoadSessionSync();

            if (session != null)
            {
                desktop.MainWindow = new MainWindow();
                log.LogInformation("MainWindow created");
                log.LogInformation("Session restored for user: {User}", session.Name);
            }
            else
            {
                // TODO: Implement Login Screen since we need it ASAP.
                log.LogWarning("No session found. Launching login screen");
                desktop.MainWindow = new LoginWindow();
            }
        }
        catch (Exception ex)
        {
            log.LogCritical(ex, "A critical error occured during application startup.");
            // TODO: Add error dialog.
        }
        finally
        {
            base.OnFrameworkInitializationCompleted();
            log.LogInformation("OnFrameworkInitializationCompleted finished");
        }
    }
}