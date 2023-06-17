using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

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

        public async Task<User> UpdateProfileAsync(UserRegistrationDto userRegistrationDto, User existingUser)
        {
            _mapper.Map(userRegistrationDto, existingUser);

            await _repositoryWrapper.User.UpdateAsync(existingUser);
            await _repositoryWrapper.SaveAsync();

            return existingUser;
        }

        public async Task DeleteProfileAsync(User user)
        {
            if (user != null)
            {
                await _repositoryWrapper.User.DeleteAsync(user);
                await _repositoryWrapper.SaveAsync();
            }
        }
    }
}
