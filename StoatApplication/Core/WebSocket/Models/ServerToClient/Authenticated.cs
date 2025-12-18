namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record Authenticated : IStoatEvent
{
    public string Type => "Authenticated";
}