namespace dotnet_rideShare.Contexts;

using Microsoft.AspNetCore.SignalR;

public class NotificationContext : Hub
{
    // Define hub methods for sending notifications
    public async Task SendNotification(string recipient, string message)
    {
        await Clients.User(recipient).SendAsync("ReceiveNotification", message);
    }
}