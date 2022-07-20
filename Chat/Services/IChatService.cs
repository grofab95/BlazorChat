using System;
using System.Threading.Tasks;

namespace Chat.Services;

public interface IChatService
{
    Task SendMessage(Message message);
    Task<Message[]> GetMessages();

    void InvokeMessageReceived(Message message);

    event EventHandler<Message> MessageReceived;
}