using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IRepositoryBase<User> _repository;
        private readonly IMapper _mapper;

        public UserRegistrationService(IRepositoryBase<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            var users = await _repository.FindAllAsync();
            if (users.Any(u => u.Login == userRegistrationDto.Login))
            {
                throw new Exception("A user with this login already exists.");
            }

            PasswordHasher.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userRegistrationDto);

            await _repository.CreateAsync(user);

            return user;
        }
    }
}
