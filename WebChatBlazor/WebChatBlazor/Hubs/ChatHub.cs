using Microsoft.AspNetCore.SignalR;
using WebChatBlazor.Client.Models;
using WebChatBlazor.Repos;
using WebChatBlazor.Shared.Models;
namespace WebChatBlazor.Hubs
{
    public class ChatHub(ChatRepo chatRepo) : Hub
    {
        public async Task SendMessage(Chat chat)
        {
            await chatRepo.SaveChatAsync(chat);
            await Clients.All.SendAsync("ReceiveMessage", chat);
        }
        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            await chatRepo.AddAvailableUserAsync(availableUser);
            var users = chatRepo.GetAvailableUsersAsync();
            await Clients.All.SendAsync("NotifyAllClients", users);
        }

    }
}
