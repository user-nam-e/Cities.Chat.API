using Cities.Chat.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "Cities.Chat.API");
app.MapHub<CommunicationHub>("/chat");

app.Run();
