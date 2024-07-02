using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebChatBlazor.Shared.Models;


public class Chat
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? MessageInput { get; set; }
    
}