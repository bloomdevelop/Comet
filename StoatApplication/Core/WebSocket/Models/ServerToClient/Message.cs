namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public record Message : IEvent
{
    public string Type => "Message";
}