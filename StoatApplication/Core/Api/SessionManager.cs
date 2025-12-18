using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api.Models;
using StoatApplication.Core.Logging;
using StoatApplication.Core.Utils;

namespace StoatApplication.Core.Api;

public static class SessionManager
{
    public static Auth.LoginResponse? CurrentSession { get; private set; }

    /// <summary>
    ///     Saves the given session information to a file and updates the current session.
    /// </summary>
    /// <param name="session">The session object containing authentication details to be saved.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task SaveSession(Auth.LoginResponse session)
    {
        var log = Logger.Create("SessionManager");

        CurrentSession = session;
        var filePath = StorageUtils.GetSessionFilePath();
        var json = JsonSerializer.Serialize(session);

        await File.WriteAllTextAsync(filePath, json);

        log.LogInformation("Successfully created session file at {FilePath}", filePath);
    }

    /// <summary>
    ///     Loads the current session information from a file.
    /// </summary>
    /// <returns>
    ///     The loaded session object containing authentication details or null if the file does not exist or an error
    ///     occurs during loading.
    /// </returns>
    public static async Task<Auth.LoginResponse?> LoadSessionAsync()
    {
        var filePath = StorageUtils.GetSessionFilePath();
        if (!File.Exists(filePath)) return null;

        try
        {
            var json = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrWhiteSpace(json)) return null;

            CurrentSession = JsonSerializer.Deserialize<Auth.LoginResponse>(json);
            return CurrentSession;
        }
        catch (Exception ex)
        {
            var log = Logger.Create("SessionManager");
            log.LogError("Failed to load session from {FilePath}: {Expection}", filePath, ex.Message);
            return null;
        }
    }

    public static Auth.LoginResponse? LoadSessionSync()
    {
        var filePath = StorageUtils.GetSessionFilePath();
        if (!File.Exists(filePath)) return null;

        try
        {
            var json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json)) return null;

            CurrentSession = JsonSerializer.Deserialize<Auth.LoginResponse>(json);
            return CurrentSession;
        }
        catch (Exception ex)
        {
            var log = Logger.Create("SessionManager");
            log.LogError("Failed to load session from {FilePath}: {Expection}", filePath, ex.Message);
            return null;
        }
    }

    /// <summary>
    ///     Clears the current session by resetting the session object to null and deleting the persisted session file if it
    ///     exists.
    /// </summary>
    public static void ClearSession()
    {
        CurrentSession = null;
        var filePath = StorageUtils.GetSessionFilePath();
        if (File.Exists(filePath)) File.Delete(filePath);
    }
}