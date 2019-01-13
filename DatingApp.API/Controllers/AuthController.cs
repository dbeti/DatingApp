using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            userDTO.UserName = userDTO.UserName.ToLower();

            var userExists = await _authService.UserExists(userDTO.UserName);

            if (userExists)
            {
                return BadRequest("User already exists!");
            }

            var createdUser = await _authService.Register(userDTO);

            return StatusCode(201);
        }
    }
}