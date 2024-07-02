using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChatBlazor.Repos;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChatAsync()
            => Ok(await chatRepo.GetChatAsync());
    }
}
