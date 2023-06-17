using AutoMapper;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(ITokenService tokenService, IProfileService profileService, IUserService userService, IMapper mapper)
        {
            _tokenService = tokenService;
            _profileService = profileService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserRegistrationDto>> GetProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);
            var user = _userService.GetUserByIdAsync(userId);

            var userRegistrationDto = _mapper.Map<UserRegistrationDto>(user);

            return Ok(userRegistrationDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserRegistrationDto>> UpdateProfileAsync(UserRegistrationDto userRegistrationDto)
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var existingUser = await _userService.GetUserByIdAsync(userId);
            var updatedUser = await _profileService.UpdateProfileAsync(userRegistrationDto, existingUser);

            var updatedUserDto = _mapper.Map<UserRegistrationDto>(updatedUser);

            return Ok(updatedUserDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync()
        {
            int userId = _tokenService.GetUserIdFromClaims(User);

            var existingUser = await _userService.GetUserByIdAsync(userId);

            await _profileService.DeleteProfileAsync(existingUser);

            return Unauthorized();
        }
    }
}