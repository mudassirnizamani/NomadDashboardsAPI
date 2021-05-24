using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NomadDashboardsAPI.Data;
using NomadDashboardsAPI.Models;
using NomadDashboardsAPI.ViewModels;
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
        private readonly UserContext _userContext;

        public UsersController(UserManager<User> userManager, UserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(i => i.Type == "UserID").Value;
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return Ok(new { succeeded = true, user = user });
            }
            catch (Exception)
            {
                return Ok(new { succeeded = false, code = "ServerError", description = "Something went wrong in Server !" });
            }

        }

        
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserByUserName/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AdminUpdateUser")]
        public async Task<IActionResult> AdminUpdateUser(AdminUpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Website = model.Website;
                user.Country = model.Country;
                user.City = model.City;
                user.PhoneNumber = model.PhoneNumber;
                user.ComponyName = model.ComponyName;

                await _userManager.UpdateAsync(user);

                return Ok(new { succeeded = true, message = $"Username '{user.UserName}' is Updated" });
            }
            else
            {
                return BadRequest(new { succeeded = false });
            }

        }

    }
}