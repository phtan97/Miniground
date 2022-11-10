using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Interfaces
{
    public interface IFootballFieldService
    {
        Task<ErrorObject> GetFootBallFields();
        Task<ErrorObject> InsertFootBallField(string name);
        Task<ErrorObject> UpdateFootBallField(int id, string name, bool? IsActive);
    }
}
