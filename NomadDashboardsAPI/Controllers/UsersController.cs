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
                user.Position = model.Position;
                user.State = model.State;
                user.ZipCode = model.ZipCode;
                user.ComponyName = model.ComponyName;
                user.ComponyAddress = model.ComponyAddress;

                await _userManager.UpdateAsync(user);

                return Ok(new {succeeded = true, message = $"Username '{user.UserName}' is Updated"});
            }
            else
            {
                return BadRequest(new {succeeded = false});
            }

        }

    }
}