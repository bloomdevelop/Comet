using System;
using System.IO;

namespace StoatApplication.Core.Utils;

public static class StorageUtils
{
    private const string AppName = "StoatApplication";

    private static string GetConfigDirectory()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }

    public static string GetSessionFilePath()
    {
        return Path.Combine(GetConfigDirectory(), "session.json");
    }
}