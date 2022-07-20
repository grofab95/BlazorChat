using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Services;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IUserService _userService;

    public ChatHub(ILogger<ChatHub> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["username"];
        
        _logger.LogInformation("{Username} connected", username ?? "<none>");
        
        await _userService.InvokeUserConnected(Context.ConnectionId, username);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var username = _userService.GetUsernameByConnectionId(Context.ConnectionId);
        
        _logger.LogInformation("{Username} disconnected", username);
        
        await _userService.InvokeUserDisconnected(Context.ConnectionId);
    }
}