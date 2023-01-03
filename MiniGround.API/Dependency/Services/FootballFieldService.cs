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
    public class FootballFieldService : IFootballFieldService
    {
        public Task<ErrorObject> GetFootBallFields()
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var footbalFields = db.TableFootBallFields.Where(x => x.IsActived == true).ToList();
                    var footballFieldIDs = footbalFields.Select(x => x.Id).ToList();
                    var fieldPrices = db.TableFieldPrice.Where(x => footballFieldIDs.Contains(x.IdFootballField)).ToList();
                    foreach(var item in footbalFields)
                    {
                        var fieldItemsPrice = fieldPrices.Where(x => x.IdFootballField == item.Id).ToList();
                        item.FieldPrices.AddRange(fieldItemsPrice);
                    }
                    return Task.FromResult(new ErrorObject(Errors.SUCCESS).SetData(footbalFields));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> InsertFootBallField(string name)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                var footBallField = new TableFootBallField
                {
                    Name = name,
                    IsActived = true                   
                };
                using (var db = new DatabaseContext())
                {
                    db.TableFootBallFields.Add(footBallField);
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Thêm thành công")) : Task.FromResult(response.Failed("Thêm sân thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> DeleteFootballField(int id)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                using(var db = new DatabaseContext())
                {
                    var footBallField = db.TableFootBallFields.FirstOrDefault(x => x.Id == id);
                    if (footBallField == null)
                    {
                        response.Failed("sân không tồn tại");
                    }
                    footBallField.IsActived = false;
                    return db.SaveChanges() > 0 ? Task.FromResult(response) : Task.FromResult(response.Failed("xoá thất bại"));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> UpdateFootBallField(int id, string name, bool? IsActive)
        {
            var response = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    var footBallField = db.TableFootBallFields.FirstOrDefault(x => x.Id == id);
                    if (footBallField == null)
                    {
                        response.Failed("sân không tồn tại");
                    }
                    footBallField.Name = name;
                    if (IsActive != null)
                    {
                        footBallField.IsActived = IsActive.Value;
                    }
                    return db.SaveChanges() > 0 ? Task.FromResult(response.SetData("Update thành công")) : Task.FromResult(response.Failed("cập nhật sân thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
