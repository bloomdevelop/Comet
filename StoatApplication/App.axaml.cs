using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Logging;

namespace StoatApplication;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var log = Logger.Create<App>();
        log.LogInformation("OnFrameworkInitializationCompleted starting");
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            log.LogInformation("MainWindow created");
        }

        base.OnFrameworkInitializationCompleted();
        log.LogInformation("OnFrameworkInitializationCompleted finished");
    }
}