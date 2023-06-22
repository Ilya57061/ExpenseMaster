using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserRegistrationService _userRegistrationService;

        public AuthController(IAuthService authService, IUserRegistrationService userRegistrationService)
        {
            _authService = authService;
            _userRegistrationService = userRegistrationService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Get(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _authService.AuthenticateAsync(userLoginDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Create(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                await _userRegistrationService.RegisterAsync(userRegistrationDto);
                return Ok(userRegistrationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
