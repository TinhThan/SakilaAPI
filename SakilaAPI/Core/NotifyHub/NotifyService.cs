using Microsoft.AspNetCore.SignalR;
using SakilaAPI.Core.NotifyHub;

namespace SakilaAPI.Core.HubSignalR
{
    public interface INotifyService
    {
        Task SendMessageNotify(string title, string message);
    }

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<NotifycationHub> _hubContext;

        public NotifyService(IHubContext<NotifycationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageNotify(string title, string message)
        {
            await _hubContext.Clients.All.SendAsync(title, message);
        }
    }
}