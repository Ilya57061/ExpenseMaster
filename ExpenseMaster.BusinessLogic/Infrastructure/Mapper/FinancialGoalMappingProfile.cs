using AutoMapper;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class FinancialGoalMappingProfile : Profile
    {
        public FinancialGoalMappingProfile()
        {
            CreateMap<CreateFinancialGoalDto, FinancialGoal>()
          .ReverseMap();

            CreateMap<UpdateFinancialGoalDto, FinancialGoal>()
                    .ReverseMap();

            CreateMap<ReturnFinancialGoalDto, FinancialGoal>()
                    .ReverseMap();
        }
    }
}
