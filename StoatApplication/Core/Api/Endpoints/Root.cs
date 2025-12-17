using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoatApplication.Core.Api.Endpoints;

public abstract class Root
{
    // TODO: Implement custom instance
    public const string ApiUrl = "https://stoat.chat/api";
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    public static readonly HttpClient Client = new();

    public static async Task<Models.Root> GetServerConfiguration()
    {
        var json = await Client.GetStringAsync($"{ApiUrl}/");

        var result = JsonSerializer.Deserialize<Models.Root>(json, Options);
        return result ?? throw new Exception("Failed to deserialize server configuration");
    }
}