using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace StoatApplication.Core.Logging;

public static class Logger
{
    private static readonly Lock Sync = new();
    private static ILoggerFactory? _factory;

    public static void Initialize(Action<ILoggingBuilder> configure)
    {
        if (_factory != null)
            return; // already initialized

        lock (Sync)
        {
            _factory ??= LoggerFactory.Create(configure);
        }
    }

    public static ILogger Create(string category)
    {
        EnsureInitialized();
        return _factory!.CreateLogger(category);
    }

    public static ILogger<T> Create<T>()
    {
        EnsureInitialized();
        return _factory!.CreateLogger<T>();
    }

    private static void EnsureInitialized()
    {
        if (_factory == null)
            throw new InvalidOperationException(
                "Logger is not initialized. Call Logger.Initialize() during application startup.");
    }
}