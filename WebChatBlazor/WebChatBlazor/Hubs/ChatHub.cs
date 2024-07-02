using Microsoft.AspNetCore.SignalR;
using WebChatBlazor.Repos;
using WebChatBlazor.Shared.Models;
namespace WebChatBlazor.Hubs
{
    public class ChatHub(ChatRepo chatRepo) : Hub
    {
        public async Task SendMessage (Chat chat)
        {
            await chatRepo.SaveChatAsync (chat);
            await Clients.All.SendAsync ("ReceiveMessage", chat);
        }
    }
}
