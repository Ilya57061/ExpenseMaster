using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExpenseMaster.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_repositoryWrapperMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Login = "user1", Email = "user1@example.com", RoleId = 1 },
                new User { Id = 2, Login = "user2", Email = "user2@example.com", RoleId = 2 }
            };

            var usersDto = users.Select(u => new UserDto { Id = u.Id, Login = u.Login, Email = u.Email, RoleId = u.RoleId });

            _repositoryWrapperMock.Setup(repo => repo.User.FindAllAsync()).ReturnsAsync(users.AsQueryable());
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(users)).Returns(usersDto);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            result.Should().HaveCount(usersDto.Count());
            result.Should().BeEquivalentTo(usersDto, options =>
    options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetUsersByLoginAsync_ValidLogin_ReturnsMatchingUsers()
        {
            // Arrange
            string login = "user";

            var users = new List<User>
            {
                new User { Id = 1, Login = "user1", Email = "user1@example.com", RoleId = 1 },
                new User { Id = 2, Login = "user2", Email = "user2@example.com", RoleId = 2 }
            };

            var usersDto = users.Select(u => new UserDto { Id = u.Id, Login = u.Login, Email = u.Email, RoleId = u.RoleId });

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Login.ToLower().Contains(login.ToLower()))).ReturnsAsync(users.AsQueryable());
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(users)).Returns(usersDto);

            // Act
            var result = await _userService.GetUsersByLoginAsync(login);

            // Assert
            result.Should().HaveCount(usersDto.Count());
            result.Should().BeEquivalentTo(usersDto, options =>
    options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetUsersByLoginAsync_NonExistingLogin_ThrowsInvalidOperationException()
        {
            // Arrange
            string login = "nonexistinguser";

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Login.ToLower().Contains(login.ToLower()))).ReturnsAsync(new List<User>().AsQueryable());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.GetUsersByLoginAsync(login));
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsMatchingUser()
        {
            // Arrange
            int id = 1;

            var user = new User { Id = id, Login = "user1", Email = "user1@example.com", RoleId = 1 };
            var userDto = new UserDto { Id = user.Id, Login = user.Login, Email = user.Email, RoleId = user.RoleId };

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == id)).ReturnsAsync(new List<User> { user }.AsQueryable());
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _userService.GetUserByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public async Task GetUserByIdAsync_NonExistingId_ThrowsInvalidOperationException()
        {
            // Arrange
            int id = 999;

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == id)).ReturnsAsync(new List<User>().AsQueryable());

            // Act & Assert
            await _userService.Invoking(async x => await x.GetUserByIdAsync(id))
    .Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task DeleteUserAsync_ExistingUserId_DeletesUser()
        {
            // Arrange
            int userId = 1;

            var existingUser = new User { Id = userId };
            var users = new List<User> { existingUser };

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userId)).ReturnsAsync(users.AsQueryable());

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _repositoryWrapperMock.Verify(repo => repo.User.DeleteAsync(existingUser), Times.Once);
            _repositoryWrapperMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_NonExistingUserId_ThrowsInvalidOperationException()
        {
            // Arrange
            int userId = 999;

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(x => x.Id == userId)).ReturnsAsync(new List<User>().AsQueryable());

            // Act & Assert
            await _userService.Invoking(async x => await x.DeleteUserAsync(userId))
    .Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task UpdateUserAsync_ExistingUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = 1,
                Login = "updateduser",
                Email = "updateduser@example.com",
                Password = "newpassword",
                RoleId = 2
            };

            var existingUser = new User { Id = userUpdateDto.Id, Login = "user1", Email = "user1@example.com", RoleId = 1 };
            var updatedUser = new User { Id = userUpdateDto.Id, Login = userUpdateDto.Login, Email = userUpdateDto.Email, RoleId = userUpdateDto.RoleId };
            var userDto = new UserDto { Id = updatedUser.Id, Login = updatedUser.Login, Email = updatedUser.Email, RoleId = updatedUser.RoleId };

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userUpdateDto.Id)).ReturnsAsync(new List<User> { existingUser }.AsQueryable());
            _repositoryWrapperMock.Setup(repo => repo.User.UpdateAsync(updatedUser)).Returns(Task.CompletedTask);
            _repositoryWrapperMock.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<User>(userUpdateDto)).Returns(updatedUser);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(updatedUser)).Returns(userDto);

            // Act
            var result = await _userService.UpdateUserAsync(userUpdateDto);

            // Assert
            result.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public async Task UpdateUserAsync_NonExistingUser_ThrowsInvalidOperationException()
        {
            // Arrange
            var userUpdateDto = new UserUpdateDto
            {
                Id = 999,
                Login = "updateduser",
                Email = "updateduser@example.com",
                Password = "newpassword",
                RoleId = 2
            };

            _repositoryWrapperMock.Setup(repo => repo.User.FindByConditionAsync(u => u.Id == userUpdateDto.Id)).ReturnsAsync(new List<User>().AsQueryable());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.UpdateUserAsync(userUpdateDto));
        }
    }
}
