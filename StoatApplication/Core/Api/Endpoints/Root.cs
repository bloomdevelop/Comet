using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Logging;
using static StoatApplication.Core.Api.ApiClient;

namespace StoatApplication.Core.Api.Endpoints;

public abstract class Root
{
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