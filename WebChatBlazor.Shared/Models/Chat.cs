using System.ComponentModel.DataAnnotations;

namespace WebChatBlazor.Shared.Models
{
    public class Chat
    {
        [Required]
        public string? MessageInput { get; set; }

        [Required]
        public string? UserName { get; set; }

        
    }
}