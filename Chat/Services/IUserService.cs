using System;
using System.Threading.Tasks;

namespace Chat.Services;

public interface IUserService
{
    Task<string[]> GetUsernames();

    void InvokeUserConnected(string username);
    void InvokeUserDisconnected(string username);

    event EventHandler<string> OnUserConnected;
    event EventHandler<string> OnUserDisconnected;
}