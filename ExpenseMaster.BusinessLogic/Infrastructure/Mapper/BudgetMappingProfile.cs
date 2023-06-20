using AutoMapper;
using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMaster.BusinessLogic.Infrastructure.Mapper
{
    public class BudgetMappingProfile : Profile
    {
        public BudgetMappingProfile()
        {
            CreateMap<Budget, CreateBudgetDto>()
                .ReverseMap();

            CreateMap<Budget, UpdateBudgetDto>()
                .ReverseMap();

            CreateMap<Budget, BudgetDto>()
                .ReverseMap();
        }
    }
}
