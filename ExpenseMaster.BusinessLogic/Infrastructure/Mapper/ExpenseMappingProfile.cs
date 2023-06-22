using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<ExpenseDto, Expense>()
                .ReverseMap();

            CreateMap<ExpenseItemDto, Expense>()
                .ReverseMap();
        }
    }
}
