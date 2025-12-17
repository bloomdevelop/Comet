using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StoatApplication.Core.Api.Endpoints;

namespace StoatApplication;

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
}