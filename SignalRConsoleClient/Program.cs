using System;
using Cities.Chat.API.Models;
using Microsoft.AspNetCore.SignalR.Client;


//var hubConnection = new HubConnectionBuilder().WithUrl("https://citieschatapi.azurewebsites.net/chat").Build();
var hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:1488/chat").Build();

#region GetConnectedUsers
hubConnection.On<string, string, string>("ReceiveMessageToUser", (user, targetConnectionId, message) => Console.WriteLine($"Message from {user} {targetConnectionId}: {message}"));

hubConnection.On<List<string>>("GetConnectedUsers", (users) =>
{
    Console.Write(users.Count + "\t");

    foreach (var item in users)
    {
        if (item != hubConnection.ConnectionId)
        {
            Console.WriteLine(item);
        }
    }
});
#endregion

hubConnection.On<string, string>("ReceiveMessage", (message, name) => Console.WriteLine($"Message from {name}: {message}"));

hubConnection.On<List<City>>("ReceiveCities", (Cities) =>
{
    foreach (var item in Cities)
    {
        Console.WriteLine($"\n {item.Id} {item.CityName}({item.CountryName}){item.FlagId}");
    }
});


await hubConnection.StartAsync();

/// /////////////////////////////////////////////////////

while (true)
{
    Console.WriteLine(hubConnection.ConnectionId);
    Console.Write("(/city /all /users /reg): ");
    var command = Console.ReadLine();

    if (command == "/city")
    {
        await hubConnection.SendAsync("GetCities");
    }
    else if (command == "/all")
    {
        Console.Write("Введите имя: ");
        var name = Console.ReadLine();

        Console.Write("Введите сообщение: ");
        var message = Console.ReadLine();

        await hubConnection.SendAsync("SendMessageToAll", message, name);
    }
    else if (command == "/users")
    {
        await hubConnection.SendAsync("SendConnectedUsers");
    }
}