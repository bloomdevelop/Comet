using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Logging;

namespace StoatApplication.Core.Api.Endpoints;

public abstract class Root
{
    // TODO: Implement custom instance
    public const string ApiUrl = "https://stoat.chat/api";
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    public static readonly HttpClient Client = new();

    public static async Task<Models.Root> GetServerConfiguration()
    {
        var log = Logger.Create("Api.Root");
        try
        {
            log.LogDebug("Requesting server configuration from {Url}", $"{ApiUrl}/");
            var json = await Client.GetStringAsync($"{ApiUrl}/");
            var result = JsonSerializer.Deserialize<Models.Root>(json, Options);
            if (result == null)
            {
                log.LogError("Failed to deserialize server configuration");
                throw new Exception("Failed to deserialize server configuration");
            }

            log.LogInformation("Fetched server configuration successfully");
            return result;
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error fetching server configuration");
            throw;
        }
    }
}