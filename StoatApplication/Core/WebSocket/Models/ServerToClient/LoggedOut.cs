namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record LoggedOut : IEvent
{
    public string Type => "LoggedOut";
}