using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;
using ExpenseMaster.Model.Models;

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
                throw new Exception("A user with this login already exists.");
            }

            PasswordHasher.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userRegistrationDto);

            await _wrapper.User.CreateAsync(user);

            await _wrapper.SaveAsync();

            return user;
        }
    }
}
