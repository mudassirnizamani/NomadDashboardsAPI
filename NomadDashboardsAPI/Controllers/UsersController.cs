using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NomadDashboardsAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NomadDashboardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(i => i.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Email,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.Website,
                user.Position,
                user.ComponyName,
                user.ZipCode,
                user.State,
                user.PhoneNumber,
                user.Country,
                user.City,
                user.ComponyAddress
            };
        }
    }
}