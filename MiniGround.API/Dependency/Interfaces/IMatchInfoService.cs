using MiniGround.API.Models;
using MiniGround.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Interfaces
{
    public interface IMatchInfoService
    {
        Task<ErrorObject> CreateMatch(CreateMatchModel matchInfo);
        Task<ErrorObject> SearchMathInfo(SearchMathInfoModel search);
        Task<ErrorObject> GetMatchInfoById(int id);
        Task<ErrorObject> UpdateMatchInfo(int id, TimeSpan timePlus);
        Task<ErrorObject> UpdateStatusMatchInfo(int idMatch, EMatchInfoStatus status);
        Task<ErrorObject> ShareBounusAfterMatchEnd(int idMath);
    }
}
