using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StoatApplication.Core.Api;
using Root = StoatApplication.Core.Api.Endpoints.Root;

namespace StoatApplication.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Button_GetServerConfiguration(object? sender, RoutedEventArgs e)
    {
        try
        {
            Label.Content = "Fetching...";

            var config = await Root.GetServerConfiguration();

            Label.Content = $"Server Version: {config.Version}";
        }
        catch (Exception ex)
        {
            Label.Content = ex.Message;
        }
    }

    private void Button_ClearSession(object? sender, RoutedEventArgs e)
    {
        try
        {
            SessionManager.ClearSession();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to clear session", ex);
        }
    }
}