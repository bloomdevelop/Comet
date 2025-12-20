using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api;
using StoatApplication.Core.Logging;
using StoatApplication.Core.WebSocket;
using Root = StoatApplication.Core.Api.Endpoints.Root;

namespace StoatApplication.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        if (SessionManager.CurrentSession == null)
            // If no session, we probably shouldn't be here if App handles it,
            // but let's check for a token regardless as requested.
            await SessionManager.LoadSessionAsync();

        if (SessionManager.CurrentSession?.Token != null)
            try
            {
                if (App.WebSocket == null)
                {
                    App.WebSocket = await WebSocketClient.CreateFromConfigAsync();
                    await App.WebSocket.ConnectAsync();
                }
            }
            catch (Exception ex)
            {
                var log = Logger.Create<MainWindow>();
                log.LogError(ex, "WebSocket connection failed");
            }
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