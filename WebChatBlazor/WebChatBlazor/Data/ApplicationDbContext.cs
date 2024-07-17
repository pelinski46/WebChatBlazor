using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebChatBlazor.Client.Models;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Data;


public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<AvailableUser> AvailableUsers { get; set; }

}
