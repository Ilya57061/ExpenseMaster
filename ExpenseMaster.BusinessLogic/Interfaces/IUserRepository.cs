﻿using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByLoginAsync(string login);
    }
}
