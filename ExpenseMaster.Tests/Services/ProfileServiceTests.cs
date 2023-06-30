using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Moq;
using Xunit;

namespace ExpenseMaster.Tests.Services
{
    public class ProfileServiceTests
    {
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProfileService _profileService;
        
        private const int userId = 1;

        public ProfileServiceTests()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _mapperMock = new Mock<IMapper>();
            _profileService = new ProfileService(_repositoryWrapperMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task UpdatePasswordAsync_IncorrectOldPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            var existingUser = new User { Id = userId, PasswordHash = new byte[64], PasswordSalt = new byte[128] };
            var updatePasswordDto = new UpdateProfilePasswordDto
            {
                OldPassword = "incorrectpassword",
                NewPassword = "newpassword",
                ConfirmPassword = "newpassword"
            };

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userId))
                .ReturnsAsync(new List<User> { existingUser }.AsQueryable());

            PasswordHasher.VerifyPasswordHash(updatePasswordDto.OldPassword, existingUser.PasswordSalt, existingUser.PasswordHash);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _profileService.UpdatePasswordAsync(updatePasswordDto, userId));

            _repositoryWrapperMock.Verify(repo => repo.User.UpdateAsync(existingUser), Times.Never);
            _repositoryWrapperMock.Verify(repo => repo.SaveAsync(), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<ProfileDto>(existingUser), Times.Never);
        }

        [Fact]
        public async Task UpdateProfileAsync_InvalidEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            var profileDto = new ProfileDto { Email = "newemail@example.com", Login = "newlogin" };
            var existingUser = new User { Id = userId, Email = "oldemail@example.com", Login = "oldlogin" };
            var emailExistsUser = new User { Id = 2, Email = profileDto.Email };
            var usersQuery = new List<User> { existingUser }.AsQueryable();
            var emailExistsQuery = new List<User> { emailExistsUser }.AsQueryable();

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userId))
                .ReturnsAsync(usersQuery);
            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Email == profileDto.Email))
                .ReturnsAsync(emailExistsQuery);
            _mapperMock.Setup(mapper => mapper.Map(profileDto, existingUser)).Verifiable();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _profileService.UpdateProfileAsync(profileDto, userId);
            });

            _repositoryWrapperMock.Verify(repo => repo.User.UpdateAsync(It.IsAny<User>()), Times.Never);
            _repositoryWrapperMock.Verify(repo => repo.SaveAsync(), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<User, ProfileDto>(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task DeleteProfileAsync_ExistingUser_DeletesUser()
        {
            // Arrange
            var existingUser = new User { Id = userId };
            var usersQuery = new List<User> { existingUser }.AsQueryable();

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userId))
                .ReturnsAsync(usersQuery);
            _repositoryWrapperMock.Setup(repo => repo.User.DeleteAsync(existingUser)).Verifiable();
            _repositoryWrapperMock.Setup(repo => repo.SaveAsync()).Verifiable();

            // Act
            await _profileService.DeleteProfileAsync(userId);

            // Assert
            _repositoryWrapperMock.Verify();
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingUser_ReturnsProfileDto()
        {
            // Arrange
            var existingUser = new User { Id = userId };
            var profileDto = new ProfileDto { Email = existingUser.Email, Login = existingUser.Login };
            var usersQuery = new List<User> { existingUser }.AsQueryable();

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userId))
                .ReturnsAsync(usersQuery);
            _mapperMock.Setup(mapper => mapper.Map<ProfileDto>(existingUser)).Returns(profileDto);

            // Act
            var result = await _profileService.GetUserByIdAsync(userId);

            // Assert
            _repositoryWrapperMock.Verify();
            _mapperMock.Verify();

            Assert.Equal(profileDto, result);
        }
    }
}
