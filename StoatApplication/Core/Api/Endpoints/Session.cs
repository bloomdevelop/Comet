using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StoatApplication.Core.Api.Models;

namespace StoatApplication.Core.Api.Endpoints;

public abstract class Session
{
    private static readonly string Url = Root.ApiUrl;
    private static readonly HttpClient Client = Root.Client;

    public static async Task<Auth.LoginResponse?> Login(string email, string password, string? friendlyName = null)
    {
        var response = await Client.PostAsJsonAsync($"{Url}/auth/session/login",
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