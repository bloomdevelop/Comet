using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StoatApplication.Core.Api;
using StoatApplication.Core.Utils;
using Root = StoatApplication.Core.Api.Endpoints.Root;

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

    private async void Button_Login(object? sender, RoutedEventArgs e)
    {
        try
        {
            var email = TextBoxEmail.Text?.Trim() ?? string.Empty;
            var password = TextBoxPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Label.Content = "Email and password cannot be empty and is required.";
                return;
            }

            if (!RegexUtilties.IsValidEmail(email))
            {
                Label.Content = "Invalid email address.";
                return;
            }

            try
            {
                Label.Content = "Logging in...";
                await ApiClient.LoginAsync(email, password);
                Label.Content = "Login successful!";
            }
            catch (Exception ex)
            {
                Label.Content = $"Error: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            Label.Content = ex.Message;
        }
    }
}