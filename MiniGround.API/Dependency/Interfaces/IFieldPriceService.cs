using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Interfaces
{
    public interface IFieldPriceService
    {
        Task<ErrorObject> GetFieldPriceByFootBallField(int footballFieldId);
        Task<ErrorObject> UpdateFiedPriceByFootBallField(int footballFieldId, double price);
        Task<ErrorObject> InsertFiedPriceByFootBallField(int footballFieldId, TimeSpan startDate, TimeSpan endDate, double price);
    }
}
