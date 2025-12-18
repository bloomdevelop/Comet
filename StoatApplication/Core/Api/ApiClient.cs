using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StoatApplication.Core.Api.Endpoints;

namespace StoatApplication.Core.Api;

public abstract class ApiClient
{
    //TODO: Implement custom instances
    public const string ApiUrl = "https://stoat.chat/api";
    public static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    public static readonly HttpClient Client = new();

    public async Task LoginAsync(string email, string password)
    {
        var response = await Session.Login(email, password);

        if (response is not null) await SessionManager.SaveSession(response);
    }
}