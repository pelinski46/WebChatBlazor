using Microsoft.AspNetCore.SignalR;
using WebChatBlazor.Shared.Models;
namespace WebChatBlazor.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage (Chat chat)
        {
            await Clients.All.SendAsync ("ReceiveMessage", chat);
        }
    }
}
