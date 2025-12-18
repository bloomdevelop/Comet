namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record Authenticated : IEvent
{
    public string Type => "Authenticated";
}