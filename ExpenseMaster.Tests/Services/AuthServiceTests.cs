using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Moq;
using System.Security.Cryptography;
using Xunit;

namespace ExpenseMaster.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _mapperMock = new Mock<IMapper>();
            _authService = new AuthService(_repositoryWrapperMock.Object, _tokenServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ExistingUser_ReturnsSuccessLoginDto()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Login = "testuser", Password = "password" };
            
            byte[] hash;
            byte[] salt;

            PasswordHasher.CreatePasswordHash(userLoginDto.Password,out hash,out salt);
            
            var existingUser = new User { Id = 1, Login = "testuser", PasswordSalt = salt, PasswordHash = hash };
            var token = "testtoken";
            var userDto = new UserDto { Id = 1, Login = "testuser", Email = "testuser@example.com", RoleId = 1 };
            var successLoginDto = new SuccesLoginDto { Token = token, UserDto = userDto };

            _repositoryWrapperMock.Setup(repo => repo.User.GetUserByLoginAsync(userLoginDto.Login))
                .ReturnsAsync(existingUser);
            _tokenServiceMock.Setup(service => service.GetToken(existingUser))
                .Returns(token);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(existingUser))
                .Returns(userDto);

            // Act
            var result = await _authService.AuthenticateAsync(userLoginDto);

            // Assert
            Assert.Equal(successLoginDto.Token, result.Token);
            Assert.Equal(successLoginDto.UserDto, result.UserDto);
        }

        [Fact]
        public async Task AuthenticateAsync_IncorrectPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Login = "testuser", Password = "incorrectpassword" };
            var existingUser = new User { Id = 1, Login = "testuser", PasswordSalt = GetRandomSalt(), PasswordHash = GetRandomHash() };

            _repositoryWrapperMock.Setup(repo => repo.User.GetUserByLoginAsync(userLoginDto.Login))
                .ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.AuthenticateAsync(userLoginDto));
        }

        private byte[] GetRandomSalt()
        {
            byte[] salt = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private byte[] GetRandomHash()
        {
            byte[] hash = new byte[64];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(hash);
            }

            return hash;
        }
    }
}
