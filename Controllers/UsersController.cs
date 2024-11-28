using CarRental_System.Filters;
using CarRental_System.Models;
using CarRental_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService) {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            try
            {
                if(user == null || !ModelState.IsValid)
                {
                    throw new Exception("Error Occurred");
                } 
                userService.RegisterUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserDTO user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Error while Logging in..");
            }
            try
            {
                return Ok(userService.AuthenticateUser(user.Email, user.Password));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                return Ok(userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        //[HttpGet("{email}")]
        //public IActionResult GetUserByEmail(string email)
        //{
        //    try
        //    {
        //        return Ok(userService.GetUserByEmail(email));
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //}


        //[Http]

    }
}
