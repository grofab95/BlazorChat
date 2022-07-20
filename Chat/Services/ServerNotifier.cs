using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Services;

public class ServerNotifier : IServerNotifier
{
    private readonly IUserService _userService;
    private readonly IChatService _chatService;

    private HubConnection _hubConnection;

    public ServerNotifier(IUserService userService, IChatService chatService)
    {
        _userService = userService;
        _chatService = chatService;
    }
    
    public async Task InitializeConnection(string username)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:4000/chathub?username={username}")
            .Build();

        ConfigureReceivers();

        await _hubConnection.StartAsync();
    }
    
    private void ConfigureReceivers()
    {
        _hubConnection.On("ReceiveMessage", (Message message) =>
        {
            _chatService.InvokeMessageReceived(message);
        });

        _hubConnection.On("UserConnected", (string username) =>
        {
            _userService.InvokeUserConnected(username);
        });
        
        _hubConnection.On("UserDisconnected", (string username) =>
        {
            _userService.InvokeUserDisconnected(username);
        });
    }
    
    public async void CloseConnection()
    {
        if (_hubConnection == null)
        {
            return;
        }
        
        await _hubConnection.StopAsync();
        await _hubConnection.DisposeAsync();
    }
}