using ChatServer;
using ChatServer.Services;

SerilogHelper.AddSerilog();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();