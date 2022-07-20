using ChatServer.Controllers;

namespace ChatServer.Services;

public interface IChatService
{
    Task AddMessage(Message message);
    Task<Message[]> GetMessages();
}