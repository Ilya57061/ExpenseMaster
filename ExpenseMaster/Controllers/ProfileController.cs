using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IProfileService _profileService;

        public ProfileController(ITokenService tokenService, IProfileService profileService)
        {
            _tokenService = tokenService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<UserRegistrationDto>> GetProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var userRegistrationDto = _profileService.GetUserByIdAsync(userId);

            return Ok(userRegistrationDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserRegistrationDto>> UpdateProfileAsync(UserRegistrationDto userRegistrationDto)
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var updatedUserDto = await _profileService.UpdateProfileAsync(userRegistrationDto, userId);

            return Ok(updatedUserDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var existingUser = await _profileService.GetUserByIdAsync(userId);

            await _profileService.DeleteProfileAsync(existingUser);

            return Unauthorized();
        }
    }
}