using AutoMapper;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
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
        .ReverseMap()
        .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, UserDto>()
        .ReverseMap();
        }
    }
}
