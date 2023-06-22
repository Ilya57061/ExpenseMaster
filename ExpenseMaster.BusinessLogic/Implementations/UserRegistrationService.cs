using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.DAL.Models;
using ExpenseMaster.BusinessLogic.Dto;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _wrapper;

        public UserRegistrationService(IMapper mapper, IRepositoryWrapper wrapper)
        {
            _mapper = mapper;
            _wrapper = wrapper;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            var users = await _wrapper.User.FindAllAsync();
            if (users.Any(u => u.Login == userRegistrationDto.Login))
            {
                throw new InvalidOperationException($"User with login - {userRegistrationDto.Login} already exists.");
            }

            PasswordHasher.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(userRegistrationDto);

            await _wrapper.User.CreateAsync(user);
            await _wrapper.SaveAsync();

            return user;
        }
    }
}
