namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record LoggedOut : IStoatEvent
{
    public string Type => "LoggedOut";
}