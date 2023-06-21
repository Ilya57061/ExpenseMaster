using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()))
                .ReverseMap();
        }
    }
}
