using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiniGround.API.Controllers
{
    [Authorize]
    [Route("api/userbanks")]
    [ApiController]
    public class UserBanksController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IUserBankService _userBankService;
        public UserBanksController(IUserBankService userBankService)
        {
            _userBankService = userBankService;
        }

        [Route("getbyuser")]
        [HttpPost]
        public async Task<IActionResult> GetUserBankAccount([FromBody] int userId)
        {
            try
            {
                var response = await _userBankService.GetBanksByUser(userId);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserBankAccount([FromBody] UpdateBankAccountModel bankAccountModel)
        {
            try
            {
                var response = await _userBankService.UpdateBankAccount(bankAccountModel);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("insert")]
        [HttpPost]
        public async Task<IActionResult> InsertUserBankAccount([FromBody] CreateBankAccountModel bankAccountModel)
        {
            try
            {
                var response = await _userBankService.InsertBankAccount(bankAccountModel);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("withdraw")]
        [HttpPost]
        public async Task<IActionResult> WithdrawMoney([FromBody](int id, decimal prices) user)
        {
            try
            {
                var response = await _userBankService.WithdrawMoney(user.id, user.prices);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        [Authorize(Roles = "0")]
        [Route("withdraw/accept")]
        [HttpPost]
        public async Task<IActionResult> AcceptWithdrawMoney([FromBody](int id, int bankID, decimal prices) user)
        {
            try
            {
                var response = await _userBankService.AcceptWithdrawMoney(user.id, user.bankID, user.prices);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
