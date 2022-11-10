using MiniGround.API.ContextModels;
using MiniGround.API.ContextModels.Tables;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Dependency.Services
{
    public class FieldPriceService : IFieldPriceService
    {
        public Task<ErrorObject> GetFieldPriceByFootBallField(int footballFieldId)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    return Task.FromResult(new ErrorObject(Errors.SUCCESS).SetData(db.TableFieldPrice.FirstOrDefault(x => x.IdFootballField == footballFieldId)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> InsertFiedPriceByFootBallField(int footballFieldId, TimeSpan startDate, TimeSpan endDate, double price)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                var fieldPrice = new TableFieldPrice
                {
                    IdFootballField = footballFieldId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Price = (decimal)price
                };
                using (var db = new DatabaseContext())
                {
                    var fieldPrices = db.TableFieldPrice.Where(x => x.IdFootballField == footballFieldId).ToList();
                    if (fieldPrices.Any(x => x.StartDate <= fieldPrice.StartDate && fieldPrice.StartDate >= x.EndDate) ||
                        fieldPrices.Any(x => x.StartDate <= fieldPrice.EndDate && fieldPrice.StartDate >= x.EndDate))
                    {
                        return Task.FromResult(response.Failed("Giờ này đã được niêm yết, vui lòng chọn giờ khác"));
                    }
                    db.TableFieldPrice.Add(fieldPrice);
                    return db.SaveChanges() > 0 ? Task.FromResult(response) : Task.FromResult(response.Failed("Thêm giá thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> UpdateFiedPriceByFootBallField(int footballFieldId, double price)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    var fieldPrice = db.TableFieldPrice.FirstOrDefault(x => x.IdFootballField == footballFieldId);
                    if (fieldPrice == null)
                    {
                        return Task.FromResult(response.Failed("bảng giá sân banh này không tồn tại"));
                    }
                    fieldPrice.Price = (decimal)price;
                    return db.SaveChanges() > 0 ? Task.FromResult(response) : Task.FromResult(response.Failed("Cập nhật bảng giá thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
