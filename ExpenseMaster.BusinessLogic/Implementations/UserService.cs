using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repositoryWrapper.User.FindAllAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByLoginAsync(string login)
        {
            return await _repositoryWrapper.User.FindByConditionAsync(u => u.Login.ToLower().Contains(login.ToLower()));
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == id);
            var user = await result.FirstOrDefaultAsync();

            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            var existingUser = await _repositoryWrapper.User.FindByConditionAsync(x => x.Id == user.Id);
            var userToDelete = await existingUser.FirstOrDefaultAsync();

            if (userToDelete != null)
            {
                await _repositoryWrapper.User.DeleteAsync(userToDelete);
                await _repositoryWrapper.SaveAsync();
            }
        }

        public async Task<User> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var existingUser = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == userUpdateDto.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            var user = _mapper.Map<User>(userUpdateDto);

            await _repositoryWrapper.User.UpdateAsync(user);
            await _repositoryWrapper.SaveAsync();

            return user;
        }
    }
}
