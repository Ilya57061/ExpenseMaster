using AutoMapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _dbContext;

        public AuthService(IRepositoryWrapper wrapper, ITokenService tokenService, IMapper mapper, ApplicationDatabaseContext dbContext)
        {
            _wrapper = wrapper;
            _tokenService = tokenService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<SuccesLoginDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _wrapper.User.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user = await _dbContext.Users
    .Include(u => u.Role)
    .AsNoTracking()
    .SingleOrDefaultAsync(u => u.Id == user.Id);

            if (!PasswordHasher.VerifyPasswordHash(userLoginDto.Password, user.PasswordSalt, user.PasswordHash))
            {
                throw new Exception("Incorrect password.");
            }

            var token = _tokenService.GetToken(user);
            var userDto = _mapper.Map<UserDto>(user);
            var jwtToken = new JwtSecurityToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var role = roleClaim?.Value;

            userDto.Role = new RoleDto { Name = role };

            var succesLoginDto = new SuccesLoginDto
            {
                Token = token,
                UserDto = userDto,
            };

            return succesLoginDto;
        }
    }
}