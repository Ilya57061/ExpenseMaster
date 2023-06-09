using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<SuccesLoginDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (!PasswordHasher.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Incorrect password.");
            }

            var token = _tokenService.GetToken(user);
            var userDto = _mapper.Map<UserDto>(user);
            var succesLoginDto = new SuccesLoginDto
            {
                Token = token,
                UserDto = userDto,
            };

            return succesLoginDto;
        }
    }
}