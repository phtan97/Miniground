using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Interfaces
{
    public interface IUserService
    {
        Task<ErrorObject> Login(UserLoginModel userLogin);
        Task<ErrorObject> Register(UserRegisterModel userRegister);
        Task<ErrorObject> ActiveUser(int id, bool isActive);
        Task<ErrorObject> DeleteUser(int id, bool isDelete);
        Task<ErrorObject> GetUser(int id);
        Task<ErrorObject> GetUsers();
    }
}
