using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public AuthService(IRepositoryWrapper wrapper, ITokenService tokenService, IMapper mapper)
        {
            _wrapper = wrapper;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<SuccesLoginDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _wrapper.User.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (!PasswordHasher.VerifyPasswordHash(userLoginDto.Password, user.PasswordSalt, user.PasswordHash))
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