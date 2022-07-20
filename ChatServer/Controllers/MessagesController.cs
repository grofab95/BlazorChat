using ChatServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers;

[ApiController]
[Route("{controller}")]
public class MessagesController : ControllerBase
{
    private readonly IChatService _chatService;

    public MessagesController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public async Task<Message[]> GetMessages()
    {
        return await _chatService.GetMessages();
    }
    
    [HttpPost]
    public async Task AddMessage([FromBody] Message message)
    {
        await _chatService.AddMessage(message);
    }
}