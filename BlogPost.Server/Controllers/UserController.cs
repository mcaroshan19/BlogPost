



using BlogPost.Server.Database;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext dbContext, IConfiguration config, ILogger<UserController> logger, IUserService userService)
        {
            _dbContext = dbContext;
            _config = config;
            _logger = logger;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Pwd))
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                var result = await _userService.RegisterAsync(model);
                if (result == "User registered successfully!")
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Pwd))
            {
                return BadRequest("Invalid login data.");
            }

            try
            {
                var result = await _userService.LoginAsync(model);
                if (result == "Invalid username or password")
                    return BadRequest(result);

                return Ok(new { token = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }





    }
}




















