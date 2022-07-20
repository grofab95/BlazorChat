using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Chat.Services;

public class ChatService : IChatService
{
    public event EventHandler<Message> MessageReceived;

    private readonly HttpClient _httpClient;
    
    public ChatService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:4000");  
    }
    
    public void InvokeMessageReceived(Message message)
    {
        MessageReceived?.Invoke(this, message);
    }

    public async Task SendMessage(Message message)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("messages", message);
        }
        catch (Exception ex)
        {
            
        }
    }

    public async Task<Message[]> GetMessages()
    {
        try
        {
            var response = await _httpClient.GetAsync("messages");

            return await response.Content.ReadFromJsonAsync<Message[]>();
        }
        catch (Exception ex)
        {
            
        }

        return Array.Empty<Message>();
    }

}