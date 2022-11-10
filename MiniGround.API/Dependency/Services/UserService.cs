using MiniGround.API.ContextModels;
using MiniGround.API.ContextModels.Tables;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniGround.API.Commons;
using MiniGround.API.Models.Enums;

namespace MiniGround.API.Dependency.Services
{
    public class UserService : IUserService
    {
        private static readonly Random _Rand = new Random();
        public Task<ErrorObject> ActiveUser(int id, bool isActive)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                using (var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                    if (user == null) return Task.FromResult(response.Failed("tài khoản không tồn tại"));
                    user.IsActived = isActive;
                    return db.SaveChanges() > 0 ? Task.FromResult(response) : Task.FromResult(response.Failed("thay đổi thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> DeleteUser(int id, bool isDelete)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                using (var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                    if (user == null) return Task.FromResult(response.Failed("tài khoản không tồn tại"));
                    user.IsDeleted = isDelete;
                    return db.SaveChanges() > 0 ? Task.FromResult(response) : Task.FromResult(response.Failed("thay đổi thất bại"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetRandomReferalCode()
        {
            try
            {
                string[] strArray = new string[62]
                {
                "0", "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "a", "b",
                "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n",
                "o", "p", "q", "r", "s", "t",
                "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F",
                "G", "H", "I", "J", "K", "L",
                "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X",
                "Y", "Z"
                };
                string randCode = "";
                while (randCode.Length <= 5)
                {
                    randCode += strArray[_Rand.Next(0, strArray.Length - 1)];
                }
                return randCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<ErrorObject> GetUser(int id)
        {
            try
            {
                var response = new ErrorObject(Errors.SUCCESS);
                using (var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == id);
                    if (user == null)
                    {
                        return Task.FromResult(response.Failed("user không tồn tại"));
                    }
                    return Task.FromResult(response.SetData(new { user.Id, user.Username, user.FullName, user.NickName, user.Phone, user.Role }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> GetUsers()
        {
            try
            {
                var users = new List<TableUser>();
                using (var db = new DatabaseContext())
                {
                    users = db.TableUsers.ToList();
                }
                users.ForEach(x => x.Password = string.Empty);
                return Task.FromResult(new ErrorObject(Errors.SUCCESS).SetData(users));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> Login(UserLoginModel userLogin)
        {
            var error = new ErrorObject(Errors.SUCCESS);
            try
            {
                TableUser user = null;
                using (var db = new DatabaseContext())
                {
                    userLogin.Password = Uitilities.HashMD5(userLogin.Password);
                    user = db.TableUsers.FirstOrDefault(s => s.Username.ToLower() == userLogin.Username.ToLower() && s.Password == userLogin.Password);
                    if (user == null)
                    {
                        return Task.FromResult(error.Failed("tài khoản hoặc mật khẩu không chính xác"));
                    }
                    if (!user.IsActived)
                    {
                        return Task.FromResult(error.Failed("tài khoản chưa được kích hoạt, vui lòng liên hệ với quản trị viên"));
                    }
                    if (user.IsDeleted)
                    {
                        return Task.FromResult(error.Failed("tài khoản của bạn đã bị khóa, vui lòng liên hệ với quản trị viên"));
                    }
                }
                return Task.FromResult(error.SetData(new { user.Id, user.Username, user.Role, user.Phone, user.ReferalCode }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ErrorObject> Register(UserRegisterModel userRegister)
        {
            var error = new ErrorObject(Errors.SUCCESS);
            try
            {
                using (var db = new DatabaseContext())
                {
                    if(db.TableUsers.Any(x=>x.Username == userRegister.Username))
                    {
                        return Task.FromResult(error.Failed("Tài khoản đã tồn tại"));
                    }
                    string referalCode = GetRandomReferalCode();
                    var parentUser = db.TableUsers.FirstOrDefault(x => x.ReferalCode.Equals(userRegister.ReferalCode));
                    var user = new TableUser();
                    user.Role = (int)EUserRole.SaleAgentLv1;
                    if (parentUser != null)
                    {
                        user.ParentId = parentUser.Id;
                        user.Role = parentUser.Role + 1;
                    }
                    user.Username = userRegister.Username;
                    user.ReferalCode = referalCode;
                    user.CreatedOn = DateTime.Now;
                    user.IsActived = false;
                    user.IsDeleted = false;
                    user.Password = Uitilities.HashMD5(userRegister.Password);
                    user.Phone = userRegister.PhoneNumber;
                    user.NickName = userRegister.NickName;
                    db.TableUsers.Add(user);
                    return db.SaveChanges() > 0 ? Task.FromResult(error.SetData("tạo tài khoản thành công, vui lòng đợi hoặc liên hệ quản trị viên để kích hoạt tài khoản")) :
                        Task.FromResult(error.SetData("Tạo tài khoản thất bại, vui lòng thử lại sau"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
