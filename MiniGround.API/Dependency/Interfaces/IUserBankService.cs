using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Interfaces
{
    public interface IUserBankService
    {
        Task<ErrorObject> GetBanksByUser(int userId);
        Task<ErrorObject> InsertBankAccount(CreateBankAccountModel BankAccountModel);
        Task<ErrorObject> UpdateBankAccount(UpdateBankAccountModel BankAccountModel);
    }
}
