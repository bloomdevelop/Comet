namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public record Message : IStoatEvent
{
    public string Type => "Message";
}