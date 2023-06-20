using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ProfileService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<UserRegistrationDto> UpdateProfileAsync(UserRegistrationDto userRegistrationDto, int userId)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == userId);
            var existingUser = await result.FirstOrDefaultAsync();

            _mapper.Map(userRegistrationDto, existingUser);

            if (userRegistrationDto == null)
            {
                throw new InvalidOperationException($"User with id - {userId} not found");
            }

            await _repositoryWrapper.User.UpdateAsync(existingUser);
            await _repositoryWrapper.SaveAsync();

            var updatedUserDto = _mapper.Map<User, UserRegistrationDto>(existingUser);

            return updatedUserDto;
        }

        public async Task DeleteProfileAsync(UserRegistrationDto existingUserDto)
        {
            var existingUser = _mapper.Map<UserRegistrationDto, User>(existingUserDto);

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User {existingUserDto.Login} not found");
            }

            await _repositoryWrapper.User.DeleteAsync(existingUser);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<UserRegistrationDto> GetUserByIdAsync(int id)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == id);
            var user = await result.FirstOrDefaultAsync();
            var userRegistrationDto = _mapper.Map<UserRegistrationDto>(user);

            if (userRegistrationDto==null)
            {
                throw new InvalidOperationException($"User with id - {id} not found");
            }

            return userRegistrationDto;
        }
    }
}
