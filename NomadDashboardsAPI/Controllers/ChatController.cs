using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NomadDashboardsAPI.Data;
using NomadDashboardsAPI.HubConfig;
using NomadDashboardsAPI.Models;
using NomadDashboardsAPI.ViewModels;
using System.Threading.Tasks;

namespace NomadDashboardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly UserContext _userContext;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(UserManager<User> userManager, UserContext userContext, IHubContext<ChatHub> hubContext)
        {
            _userManager = userManager;
            _userContext = userContext;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<object> GetMessages()
        {
            var messages = await _userContext.Messages.ToListAsync();
            return Ok(messages);
        }

        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage(MessageModel model)
        {
            User user = await _userManager.FindByNameAsync(model.UserName);
            var message = new Message();

            message.UserName = user.UserName;
            message.UserID = user.Id;
            message.Text = model.Message;
            message.Sender = user;
            await _userContext.Messages.AddAsync(message);
            await _userContext.SaveChangesAsync();
            return Ok();
        }

        [Route("SendMessage")]                                           //path looks like this: https://localhost:44379/api/chats/SendMessate
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageModel model)
        {
            User user = await _userManager.FindByNameAsync(model.UserName);
            var message = new Message();
            message.UserName = user.UserName;
            message.UserID = user.Id;
            message.Text = model.Message;
            message.Sender = user;
            await _userContext.Messages.AddAsync(message);
            await _userContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("revieveMessage", message);
            return Ok();
        }


        [Route("send")]                                           //path looks like this: https://localhost:44379/api/chat/send
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageModel msg)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.UserName, msg.Message);
            return Ok();
        }

    }
}