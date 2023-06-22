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
        public async Task<ActionResult<ProfileDto>> GetProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var user = await _profileService.GetUserByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<ProfileDto>> UpdateProfileAsync(ProfileDto profileDto)
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var updatedProfileDto = await _profileService.UpdateProfileAsync(profileDto, userId);

            return Ok(updatedProfileDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            await _profileService.DeleteProfileAsync(userId);

            return Unauthorized();
        }

        [HttpPut("UpdatePassword")]
        public async Task<ActionResult<ProfileDto>> UpdatePassword(UpdateProfilePasswordDto updatePasswordDto)
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var profileDto = await _profileService.UpdatePasswordAsync(updatePasswordDto, userId);

            return Ok(profileDto);
        }
    }
}