using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public UserRegistrationService(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto userDto)
        {
            if (await _userRepository.GetUserByLoginAsync(userDto.Login) != null)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }

            _hashService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Name = userDto.Login,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.CreateUserAsync(user);

            return user;
        }

    }
}
