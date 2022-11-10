using MiniGround.API.ContextModels;
using MiniGround.API.ContextModels.Tables;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using MiniGround.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Services
{
    public class MatchInfoService : IMatchInfoService
    {
        public Task<ErrorObject> CreateMatch(CreateMatchModel matchInfoModel)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                var datetimeNow = DateTime.Now;
                var startDay = new DateTime(datetimeNow.Year, datetimeNow.Month, datetimeNow.Day);
                var endDay = startDay.AddHours(23).AddMinutes(59).AddSeconds(59);
                using (var db = new DatabaseContext())
                {
                    var matchsInDay = db.TableMatchInfos.Where(x => startDay <= x.CreatedOn && x.CreatedOn <= endDay && x.Status != (int)EMatchInfoStatus.Deleted).ToList();
                    if (matchsInDay.Any(x => x.StartTime <= matchInfoModel.StartTime && matchInfoModel.StartTime <= x.EndTime) ||
                        matchsInDay.Any(x => x.StartTime <= matchInfoModel.EndTime && matchInfoModel.EndTime <= x.EndTime))
                    {
                        return Task.FromResult(response.Failed("sân này đã được đặt, vui lòng chọn sân khác hoặc thay đổi thời gian"));
                    }
                    var matchInfo = new TableMatchInfo
                    {
                        UserId = matchInfoModel.UserId,
                        CreatedOn = DateTime.Now,
                        Status = (int)EMatchInfoStatus.Pending
                    };
                    db.TableMatchInfos.Add(matchInfo);
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Đặt sân thành công")) : Task.FromResult(response.Failed("đặt sân thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> GetMatchInfoById(int id)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                TableMatchInfo matchInfo = null;
                using(var db = new DatabaseContext())
                {
                    matchInfo = db.TableMatchInfos.FirstOrDefault(x => x.Id == id);
                }
                if(matchInfo == null)
                {
                    return Task.FromResult(response.Failed("Trận không tồn tại"));
                }
                return Task.FromResult(response.SetData(matchInfo));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> SearchMathInfo(SearchMathInfoModel search)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                using (var db = new DatabaseContext())
                {
                    if (search.Status.HasValue) {
                        response.SetData(db.TableMatchInfos.Where(x => x.CreatedOn >= search.StartDate && x.CreatedOn <= search.EndDate &&
                        x.Status == (int)search.Status && x.UserId == search.UserID).ToList()); 
                    }
                    else
                    {
                        response.SetData(db.TableMatchInfos.Where(x => x.CreatedOn >= search.StartDate && x.CreatedOn <= search.EndDate && x.UserId == search.UserID).ToList());
                    }
                }
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> ShareBounusAfterMatchEnd(int idMath)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var matchInfo = db.TableMatchInfos.FirstOrDefault(x => x.Id == idMath);
                            var matchField = db.TableFieldPrice.FirstOrDefault(x => x.IdFootballField == matchInfo.Id);
                            var userSetMatch = db.TableUsers.FirstOrDefault(x => x.Id == matchInfo.UserId);
                            if (userSetMatch.ParentId == null || userSetMatch.ParentId == 0)
                            {
                                var userBank = db.TableUserBanks.FirstOrDefault(x => x.UserId == userSetMatch.Id);
                                userBank.AccountBalance += matchField.Price * ((decimal)EUserBounus.Level4 / 100);
                            }
                            var userLevels = Enum.GetValues(typeof(EUserRole)).Cast<EUserRole>().ToArray();
                            int idUserParent = (int)userSetMatch.ParentId;
                            foreach (var item in userLevels)
                            {
                                var userParent = db.TableUsers.FirstOrDefault(x => x.ParentId == idUserParent);
                                var userBank = db.TableUserBanks.FirstOrDefault(x => x.UserId == userParent.Id);
                                userBank.AccountBalance += matchField.Price * ((decimal)item / 100);
                                idUserParent = (int)userParent.ParentId;
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Share bounus thành công")) : Task.FromResult(response.Failed("Share bounus thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> UpdateMatchInfo(int id, TimeSpan timePlus)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    var matchInfo = db.TableMatchInfos.FirstOrDefault(x => x.Id == id);
                    if (matchInfo == null)
                    {
                        return Task.FromResult(response.Failed("trận không tồn tại"));
                    }
                    matchInfo.EndTime.Add(timePlus);
                    if (db.TableMatchInfos.Any(x => matchInfo.EndTime > x.StartTime && matchInfo.EndTime < x.EndTime))
                    {
                        return Task.FromResult(response.Failed("cập nhật thất bại, đã có trận đặt vào giờ này"));
                    }
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Cập nhật trận đấu thành công")) : Task.FromResult(response.Failed("cập nhật trận thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> UpdateStatusMatchInfo(int idMatch, EMatchInfoStatus status)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    var matchInfo = db.TableMatchInfos.FirstOrDefault(x => x.Id == idMatch);
                    if (matchInfo == null)
                    {
                        return Task.FromResult(response.Failed("trận không tồn tại"));
                    }
                    matchInfo.Status = (int)status;
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Cập nhật trạng thái thành công")) : Task.FromResult(response.Failed("Cập nhật trận thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
