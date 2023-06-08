using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IRepository<User> _repository;

        public UserRegistrationService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            var users = await _repository.GetAllAsync();
            if (users.Any(u => u.Login == userRegistrationDto.Login))
                throw new Exception("Пользователь с таким логином уже существует");

            PasswordHasher.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Login = userRegistrationDto.Login,
                Email = userRegistrationDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _repository.CreateAsync(user);

            return user;
        }
    }
}
