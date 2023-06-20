using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<IncomeDto, Income>()
                .ReverseMap();

            CreateMap<IncomeItemDto, Income>()
                .ReverseMap();
        }
    }
}
