using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repositoryWrapper.User.FindAllAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByLoginAsync(string login)
        {
            var users = await _repositoryWrapper.User.FindByConditionAsync(u => u.Login.ToLower().Contains(login.ToLower()));
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            if (usersDto == null)
            {
                throw new InvalidOperationException($"User with login - {login} not found");
            }

            return usersDto;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == id);
            var user = result.FirstOrDefault();
            var userDto = _mapper.Map<UserDto>(user);

            if (user == null)
            {
                throw new InvalidOperationException($"User with id - {id} not found");
            }

            return userDto;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var existingUser = await _repositoryWrapper.User.FindByConditionAsync(x => x.Id == userId);
            var userToDelete = existingUser.FirstOrDefault();

            if (userToDelete == null)
            {
                throw new InvalidOperationException($"User with id - {userId} not found");
            }

            await _repositoryWrapper.User.DeleteAsync(userToDelete);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<UserDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var existingUser = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == userUpdateDto.Id);

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with id - {userUpdateDto.Id} not found");
            }

            var user = _mapper.Map<User>(userUpdateDto);

            await _repositoryWrapper.User.UpdateAsync(user);
            await _repositoryWrapper.SaveAsync();

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
