using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoatApplication.Core.Api.Endpoints;

public class Session
{
    private readonly string _url = Root.ApiUrl;
    private readonly HttpClient _client = Root.Client;
    
    static async Task Login(String email, String password)
    {
        
    }
}