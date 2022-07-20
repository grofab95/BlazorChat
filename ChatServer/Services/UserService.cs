using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Services;

public class UserService : IUserService
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly Dictionary<string, string> _connectionIdToUsername;

    public UserService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;

        _connectionIdToUsername = new();
    }
    
    public async Task InvokeUserConnected(string connectionId, string username)
    {
        await _hubContext.Clients.All.SendAsync("UserConnected", username);
        
        _connectionIdToUsername.Add(connectionId, username);
    }

    public async Task InvokeUserDisconnected(string connectionId)
    {
        var username = _connectionIdToUsername[connectionId];
        
        await _hubContext.Clients.All.SendAsync("UserDisconnected", username);
        
        _connectionIdToUsername.Remove(connectionId);
    }

    public string GetUsernameByConnectionId(string connectionId)
    {
        return _connectionIdToUsername[connectionId];
    }

    public Task<string[]> GetUsernames()
    {
        return Task.FromResult(_connectionIdToUsername.Select(x => x.Value).ToArray());
    }
}