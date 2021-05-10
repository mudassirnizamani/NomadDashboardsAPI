using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NomadDashboardsAPI.Data;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APIContext _apiContext;

        public AccountsController(IUserRepo repository, UserManager<User> userManager, IOptions<AppSettings> appSettings, RoleManager<IdentityRole> roleManager, APIContext aPIContext)
        {
            _repository = repository;
            _userManager = userManager;
            _appSettings = appSettings;
            _roleManager = roleManager;
            _apiContext = aPIContext;
        }

        // For Creating User as Client
        [HttpPost]
        [Route("Signup/Client")]
        public async Task<object> ClientSignup(ClientSignupModel model)
        {
            var currentDate = DateTime.Now.ToString("d/M/yyyy");

            var email = await _userManager.FindByEmailAsync(model.Email);

            if (email == null)
            {
                var appUser = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Website = model.Website,
                    ComponyName = model.ComponyName,
                    Country = model.Country,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Province = model.Province,
                    IsActive = false,
                    LastLoginIp = "",
                    CreatedAt = currentDate,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                    ProfilePic = model.ProfilePic,
                };

                var clientAnswers = new ClientQuestion()
                {
                    UserName = model.UserName,
                    Question_1_Answer = model.Answer_1,
                    Question_2_Answer = model.Answer_2,
                    Question_3_Answer = model.Answer_3,
                    Question_4_Answer = model.Answer_4,
                    Question_5_Answer = model.Answer_5,
                    Question_6_Answer = model.Answer_6,
                    Question_7_Answer = model.Answer_7,
                    Question_8_Answer = model.Answer_8,
                    Question_9_Answer = model.Answer_9,
                };

                try
                {
                    var result = await _userManager.CreateAsync(appUser, model.Password);
                    var adding = await _apiContext.AddAsync(clientAnswers);
                    if (result.Succeeded)
                    {
                        var test = await _apiContext.SaveChangesAsync();
                        var role = await _userManager.AddToRoleAsync(appUser, "Client");
                    }

                    return Ok(result);
                }
                catch (Exception e)
                {
                    return Ok(new { succeeded = false, error = new { code = "ServerNotRespond", description = "Something went wrong in the Server !" } });
                    Console.WriteLine(e)
;
                }
            }
            else
            {
                return Ok(new { succeeded = false, error = "DuplicateEmail", description = "Email '" + model.Email + "` already taken" });
            }
        }

        // For Creating User as Customer
        [HttpPost]
        [Route("Employee/Signup")]
        public async Task<object> SignupEmployeeUser(EmployeeSignupModel model)
        {
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            var appUser = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                IsActive = false,
                LastLoginIp = "",
                CreatedAt = currentDate,
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(appUser, "Employee");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { succeeded = false, message = "Something went wrong in the Server !" });
            }
        }

        [HttpGet]
        [Route("GetUserRole/{userName}")]
        public async Task<ActionResult> GetUserRole(string userName)
        {
            var appuser = await _userManager.FindByNameAsync(userName);
            if (appuser != null)
            {
                var userRole = await _userManager.GetRolesAsync(appuser);

                return Ok(new { succeeded = true, roles = userRole });
            }
            else
            {
                return BadRequest(new { succeeded = false, message = "USERNOTFOUND" });
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
                    return Ok(new { succeeded = false, code = "UsernameNotFound", description = "Username '" + model.UserName + "' was not Found" });
                }
                else if (user != null && !user.IsActive)
                {
                    return Ok(new { succeeded = false, code = "AccountNotActivated", description = "Your Account is not Activated '" + model.UserName + "', wait for your Account to be Activated" });
                }
                else if (user != null && !password)
                {
                    return Ok(new { succeeded = false, code = "IncorrectPassword", description = "Incorrect Password for '" + model.UserName + "'" });
                }
                else if (user != null && password && user.IsActive)
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

                    return Ok(new { succeeded = true, description = "Here is your Token :)", token = token });
                }
                else
                {
                    return Ok(new { succeeded = false, code = "InvalidCredentials", description = "Username or Password in Incorrect" });
                }
            }
            catch (Exception)
            {
                return Ok(new { succeeded = false, code = "ServerError", description = "Something went wrong in the Server !" });
            }
        }
    }
}