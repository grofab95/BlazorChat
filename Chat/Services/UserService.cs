using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Chat.Services;

public class UserService : IUserService
{
    public event EventHandler<string> OnUserConnected;
    public event EventHandler<string> OnUserDisconnected;
    
    private readonly HttpClient _httpClient;
    
    public UserService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:4000");  
    }
    
    public async Task<string[]> GetUsernames()
    {
        try
        {
            var response = await _httpClient.GetAsync("users");

            return await response.Content.ReadFromJsonAsync<string[]>();
        }
        catch (Exception ex)
        {
            
        }

        return Array.Empty<string>();
    }

    public void InvokeUserConnected(string username)
    {
        OnUserConnected?.Invoke(this, username);
    }

    public void InvokeUserDisconnected(string username)
    {
        OnUserDisconnected?.Invoke(this, username);
    }
}