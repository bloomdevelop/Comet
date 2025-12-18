using System;
using System.Collections.Generic;

namespace StoatApplication.Core.WebSocket.Models;

public static class EventTypeRegistry
{
    private static readonly Dictionary<string, Type> Map = new(StringComparer.Ordinal);

    public static void Register<TEvent>(string type) where TEvent : IEvent
    {
        Map[type] = typeof(TEvent);
    }

    public static bool TryResolve(string type, out Type? clrType)
    {
        return Map.TryGetValue(type, out clrType);
    }
}