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
    [Authorize(Roles = "0")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _userService.GetUsers();
                if(response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);
            }
            catch(Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "system errors");
            }
        }

        [HttpGet]
        [Route("getuser")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var response = await _userService.GetUser(id);
                if(response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "system errors");
            }
        }

        [HttpPut]
        [Route("active")]
        public async Task<IActionResult> UpdateUser(int id, bool isActive)
        {
            try
            {
                var response = await _userService.ActiveUser(id, isActive);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);

            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "system errors");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser(int id, bool isDelete)
        {
            try
            {
                var response = await _userService.DeleteUser(id, isDelete);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "system errors");
            }
        }

        [Route("get/sortbyprices")]
        [HttpGet]
        public async Task<IActionResult> GetUsersBySortPrices(DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _userService.GetUsersBySortPrice(startDate, endDate);
                if(response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "system errors");
            }
        }
    }
}
