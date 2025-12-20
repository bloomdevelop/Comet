using System;
using System.IO;

namespace StoatApplication.Core.Utils;

public static class StorageUtils
{
    private const string AppName = "Comet";

    public static string GetLocalDataPath()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }

    public static string GetSessionFilePath()
    {
        return Path.Combine(GetLocalDataPath(), "session.json");
    }
}