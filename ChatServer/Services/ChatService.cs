using ChatServer.Controllers;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Services;

public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _hubContext;

    private readonly List<Message> _messages = new();

    public ChatService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddMessage(Message message)
    {
        _messages.Add(message);

        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }

    public Task<Message[]> GetMessages()
    {
        return Task.FromResult(_messages.ToArray());
    }
}