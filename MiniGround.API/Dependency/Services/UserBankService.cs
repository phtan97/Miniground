﻿using MiniGround.API.Commons;
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
    public class UserBankService : IUserBankService
    {
        public Task<ErrorObject> GetBanksByUser(int userId)
        {
            var err = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == userId && x.IsActived == true && x.IsDeleted == false);
                    if (user == null)
                    {
                        return Task.FromResult(err.Failed("user không tồn tại hoặc chưa được kích hoạt"));
                    }
                    var userBank = db.TableUserBanks.Where(x => x.UserId == userId).ToList();
                    userBank.ForEach(x => x.Password = string.Empty);
                    return Task.FromResult(err.SetData(userBank));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> InsertBankAccount(CreateBankAccountModel BankAccountModel)
        {
            var err = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (db.TableUserBanks.Any(x => x.BankNumber == BankAccountModel.AccountNumber))
                    {
                        return Task.FromResult(err.Failed("Accunt number đã tồn tại"));
                    }
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == BankAccountModel.UserId && x.IsActived == true && x.IsDeleted == false);
                    if (user == null)
                    {
                        return Task.FromResult(err.Failed("user không tồn tại hoặc chưa được kích hoạt"));
                    }
                    db.TableUserBanks.Add(new TableUserBank
                    {
                        BankNumber = BankAccountModel.AccountNumber,
                        BankName = BankAccountModel.AccountName,
                        FullName = BankAccountModel.FullName,
                        UserId = user.Id,
                        AccountBalance = 0,
                        CreatedOn = DateTime.Now,
                        Password = Uitilities.HashMD5(BankAccountModel.Password),
                        Status = (int)EDefaultStatus.Active
                    });
                    return db.SaveChanges() > 0 ? Task.FromResult(err) : Task.FromResult(err.Failed("Thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> UpdateBankAccount(UpdateBankAccountModel BankAccountModel)
        {
            var err = new ErrorObject(Errors.SUCCESS);
            try
            {
                using(var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == BankAccountModel.UserId && x.IsActived == true && x.IsDeleted == false);
                    if (user == null)
                    {
                        return Task.FromResult(err.Failed("user không tồn tại hoặc chưa được kích hoạt"));
                    }
                    var userBank = db.TableUserBanks.FirstOrDefault(x => x.BankNumber == BankAccountModel.NumberAccount && x.UserId == BankAccountModel.UserId);
                    if(userBank == null)
                    {
                        return Task.FromResult(err.Failed("bank user không tồn tại hoặc chưa được kích hoạt"));
                    }
                    if (!userBank.Password.Equals(Uitilities.HashMD5(BankAccountModel.Password)))
                    {
                        return Task.FromResult(err.Failed("mật khẩu không chính xác"));
                    }
                    userBank.Status = (int)BankAccountModel.Status;
                    userBank.FullName = BankAccountModel.FullName;
                    if(BankAccountModel.Password.Length > 0)
                    {
                        userBank.Password = Uitilities.HashMD5(BankAccountModel.Password);
                    }
                    return db.SaveChanges() > 0 ? Task.FromResult(err) : Task.FromResult(err.Failed("cập nhật thất bại"));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
