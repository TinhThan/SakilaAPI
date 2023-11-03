using Microsoft.AspNetCore.SignalR;

namespace SakilaAPI.Core.NotifyHub
{
    public class NotifycationHub : Hub
    {
        public async Task SendMessage(string title, string message)
        {
            await Clients.All.SendAsync(title, message);
        }
    }
}
