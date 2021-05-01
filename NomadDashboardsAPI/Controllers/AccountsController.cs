using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NomadDashboardsAPI.Interfaces;
using NomadDashboardsAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NomadDashboardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private UserManager<User> _userManager;
        private readonly IOptions<AppSettings> _appSettings;

        public AccountsController(IUserRepo repository, UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _userManager = userManager;
            _appSettings = appSettings;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<object> SignupUser(SignupModel model)
        {
            var appUser = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Website = model.Website,
                Position = model.Position,
                ComponyName = model.ComponyName,
                ZipCode = model.ZipCode,
                State = model.State,
                PhoneNumber = model.PhoneNumber,
                Country = model.Country,
                City = model.City,
                ComponyAddress = model.ComponyAddress
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { succeeded = false, message = "Something went wrong in the Server !" });
            }
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<ActionResult> SigninUser(SigninModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var password = await _userManager.CheckPasswordAsync(user, model.Password);
            try
            {
                if (user == null)
                {
                    return BadRequest(new { succeeded = false, error = "USERNAMEDOESNOTEXIST", message = "Username does not exist" });
                }
                else if (user != null && !password)
                {
                    return BadRequest(new { succeeded = false, error = "PASSWORDISINCORRECT", message = "Password is Incorrect" });
                }
                else if (user != null && password)
                {
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                          new Claim("UserID", user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JET_SECRECT_KEY)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var secuarityToken = tokenHandler.CreateToken(tokenDescription);
                    var token = tokenHandler.WriteToken(secuarityToken);

                    return Ok(new { succeeded = true, message = "Here is your Token :)", token = token });
                }
                else
                {
                    return BadRequest(new { succeeded = false, message = "Username or Password in Incorrect" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { succeeded = false, message = "Something went wrong in the Server !" });
            }
        }
    }
}