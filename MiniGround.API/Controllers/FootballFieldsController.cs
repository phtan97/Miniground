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
    [Route("api/footballfields")]
    [ApiController]
    public class FootballFieldsController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFootballFieldService _footballFieldService;
        public FootballFieldsController(IFootballFieldService footballFieldService)
        {
            _footballFieldService = footballFieldService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetFootballFields()
        {
            try
            {
                var response = await _footballFieldService.GetFootBallFields();
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

        [Authorize(Roles = "0")]
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertFootballField([FromBody] string footballName)
        {
            try
            {
                var response = await _footballFieldService.InsertFootBallField(footballName);
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

        [Authorize(Roles = "0")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateFootBallField(int id, string footballName, bool isActive)
        {
            try
            {
                var response = await _footballFieldService.UpdateFootBallField(id, footballName, isActive);
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
        
        [Authorize(Roles = "0")]
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> DeleteFootBallField(int id, string footballName, bool isActive)
        {
            try
            {
                var response = await _footballFieldService.UpdateFootBallField(id, footballName, isActive);
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
    }
}
