using System.Threading.Tasks;

namespace Chat.Services;

public interface IServerNotifier
{
    Task InitializeConnection(string username);
    void CloseConnection();
}