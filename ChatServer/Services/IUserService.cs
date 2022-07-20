namespace ChatServer.Services;

public interface IUserService
{
    Task InvokeUserConnected(string connectionId, string username);
    Task InvokeUserDisconnected(string connectionId);

    string GetUsernameByConnectionId(string connectionId);

    Task<string[]> GetUsernames();
}