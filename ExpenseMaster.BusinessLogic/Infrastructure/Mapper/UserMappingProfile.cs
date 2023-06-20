using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Infrastructure.Cryptography;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegistrationDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                PasswordHasher.CreatePasswordHash(src.Password, out byte[] passwordHash, out byte[] passwordSalt);
                dest.PasswordHash = passwordHash;
                dest.PasswordSalt = passwordSalt;
            })
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(_ => 1))
            .ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, UserDto>()
        .ReverseMap();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    PasswordHasher.CreatePasswordHash(src.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    dest.PasswordHash = passwordHash;
                    dest.PasswordSalt = passwordSalt;
                })
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
