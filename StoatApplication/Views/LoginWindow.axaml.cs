using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StoatApplication.Core.Api;
using StoatApplication.Core.Utils;

namespace StoatApplication.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void HandleLogin(object? sender, RoutedEventArgs e)
    {
        try
        {
            var email = TextBoxEmail.Text?.Trim() ?? string.Empty;
            var password = TextBoxPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage.Content = "Email and password cannot be empty and is required.";
                ErrorBorder.IsVisible = true;
                return;
            }

            if (!RegexUtilties.IsValidEmail(email))
            {
                ErrorMessage.Content = "Invalid email address.";
                ErrorBorder.IsVisible = true;
                return;
            }

            try
            {
                LoginButton.IsEnabled = false;
                await ApiClient.LoginAsync(email, password);
                LoginButton.IsEnabled = true;
                // TODO: Redirect to MainWindow once logged in and connect
            }
            catch (Exception ex)
            {
                ErrorMessage.Content = $"Error: {ex.Message}";
                ErrorBorder.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.Content = ex.Message;
            ErrorBorder.IsVisible = true;
        }
    }
}