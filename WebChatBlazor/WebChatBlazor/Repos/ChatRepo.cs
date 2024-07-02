using Microsoft.EntityFrameworkCore;
using WebChatBlazor.Data;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Repos;

public class ChatRepo(ApplicationDbContext applicationDbContext)
{
    public async Task SaveChatAsync(Chat chat)
    {
        applicationDbContext.Chats.Add(chat);
        await applicationDbContext.SaveChangesAsync();
    }
    public async Task<List<Chat>> GetChatAsync()
        => await applicationDbContext.Chats.ToListAsync();
}
