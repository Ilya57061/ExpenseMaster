using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<UserDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (_hashService.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Неверный пароль");
            }
            var token = _tokenService.GetToken(user);
            var userDto = new UserDto { Login = user.Name, Email = user.Email, AuthorizationHeader = $"Bearer {token}" };
            return userDto;
        }


    }
}
