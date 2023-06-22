using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
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

        public async Task<ProfileDto> UpdateProfileAsync(ProfileDto profileDto, int userId)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == userId);
            var existingUser = await result.FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with id - {userId} not found");
            }

            if (profileDto.Email != existingUser.Email)
            {
                var emailExists = await _repositoryWrapper.User.FindByConditionAsync(u => u.Email == profileDto.Email);
                if (emailExists != null)
                {
                    throw new InvalidOperationException($"Email '{profileDto.Email}' is already associated with another user.");
                }
            }

            if (profileDto.Login != existingUser.Login)
            {
                var emailExists = await _repositoryWrapper.User.FindByConditionAsync(u => u.Login == profileDto.Login);
                if (emailExists != null)
                {
                    throw new InvalidOperationException($"Login '{profileDto.Login}' is already taken by another user.");
                }
            }

            _mapper.Map(profileDto, existingUser);

            await _repositoryWrapper.User.UpdateAsync(existingUser);
            await _repositoryWrapper.SaveAsync();

            var updatedProfileDto = _mapper.Map<User, ProfileDto>(existingUser);

            return updatedProfileDto;
        }

        public async Task DeleteProfileAsync(int userId)
        {
            var existingUser = await _repositoryWrapper.User.FindByConditionAsync(x => x.Id == userId);
            var userToDelete = await existingUser.FirstOrDefaultAsync();

            if (userToDelete == null)
            {
                throw new InvalidOperationException($"User with id - {userId} not found");
            }

            await _repositoryWrapper.User.DeleteAsync(userToDelete);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<ProfileDto> GetUserByIdAsync(int id)
        {
            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == id);
            var user = await result.FirstOrDefaultAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"User with id - {id} not found");
            }

            var profileDto = _mapper.Map<ProfileDto>(user);

            return profileDto;
        }

        public async Task<ProfileDto> UpdatePasswordAsync(UpdateProfilePasswordDto updatePasswordDto, int userId)
        {
            if (updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            var result = await _repositoryWrapper.User.FindByConditionAsync(u => u.Id == userId);
            var existingUser = await result.FirstOrDefaultAsync();

            if (!PasswordHasher.VerifyPasswordHash(updatePasswordDto.OldPassword, existingUser.PasswordSalt, existingUser.PasswordHash))
            {
                throw new InvalidOperationException("Incorrect old password!");
            }

            PasswordHasher.CreatePasswordHash(updatePasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            existingUser.PasswordSalt=passwordSalt;
            existingUser.PasswordHash=passwordHash;

            await _repositoryWrapper.User.UpdateAsync(existingUser);
            await _repositoryWrapper.SaveAsync();

            var profileDto = _mapper.Map<ProfileDto>(existingUser);

            return profileDto;
        }
    }
}
