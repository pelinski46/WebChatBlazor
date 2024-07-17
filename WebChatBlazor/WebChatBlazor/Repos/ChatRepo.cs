using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebChatBlazor.Client.DTOs;
using WebChatBlazor.Client.Models;
using WebChatBlazor.Data;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Repos;

public class ChatRepo(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
{
    public async Task SaveChatAsync(Chat chat)
    {
        applicationDbContext.Chats.Add(chat);
        await applicationDbContext.SaveChangesAsync();
    }
    public async Task<List<Chat>> GetChatAsync()
        => await applicationDbContext.Chats.ToListAsync();


    public async Task AddAvailableUserAsync(AvailableUser availableUser)
    {
        var getUser = await applicationDbContext.AvailableUsers.FirstOrDefaultAsync(
            _ => _.UserId == availableUser.UserId);
        if (getUser != null)
        {
            getUser.ConnectionId = availableUser.ConnectionId;
        }
        else
        {
            applicationDbContext.AvailableUsers.Add(availableUser);
            await applicationDbContext.SaveChangesAsync();
        }
    }
    public async Task<List<AvailableUserDTO>> GetAvailableUsersAsync()
    {
        var list = new List<AvailableUserDTO>();
        var users = await applicationDbContext.AvailableUsers.ToListAsync();
        foreach (var u in users)
        {
            list.Add(new AvailableUserDTO(
                UserId: u.UserId,
                ConnectionId: u.ConnectionId

                ));
        }
        return list;
    }
}
