using System.Net.Http.Json;
using System.Threading.Tasks;
using StoatApplication.Core.Api.Models;
using static StoatApplication.Core.Api.ApiClient;

namespace StoatApplication.Core.Api.Endpoints;

public abstract class Session
{
    public static async Task<Auth.LoginResponse?> Login(string email, string password, string? friendlyName = null)
    {
        var response = await Client.PostAsJsonAsync($"{ApiUrl}/auth/session/login",
            new
            {
                email,
                password,
                friendly_name = friendlyName ?? ""
            });

        // Ensures the response's status to be successful 
        response.EnsureSuccessStatusCode();

        return response.Content.ReadFromJsonAsync<Auth.LoginResponse>().Result;
    }
}