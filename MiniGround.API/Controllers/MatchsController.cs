using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using MiniGround.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiniGround.API.Controllers
{
    [Authorize]
    [Route("api/matches")]
    [ApiController]
    public class MatchsController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMatchInfoService _matchInfoService;
        public MatchsController(IMatchInfoService matchInfoService)
        {
            _matchInfoService = matchInfoService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchModel createMatchModel)
        {
            try
            {
                var response = await _matchInfoService.CreateMatch(createMatchModel);
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

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchMath([FromBody] SearchMathInfoModel searchMathInfoModel)
        {
            try
            {
                var response = await _matchInfoService.SearchMathInfo(searchMathInfoModel);
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

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMatch(int id)
        {
            try
            {
                var response = await _matchInfoService.GetMatchInfoById(id);
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
        [Route("update/time")]
        public async Task<IActionResult> UpdateMatch(int id, string timePlus)
        {
            try
            {
                var response = await _matchInfoService.UpdateMatchInfo(id, TimeSpan.Parse(timePlus));
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
        [Route("update/status")]
        public async Task<IActionResult> UpdateStatus(int id, EMatchInfoStatus status)
        {
            try
            {
                var response = await _matchInfoService.UpdateStatusMatchInfo(id, status);
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
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            try
            {
                var response = await _matchInfoService.UpdateStatusMatchInfo(id, EMatchInfoStatus.Deleted);
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
        [HttpPost]
        [Route("share/bounus")]
        public async Task<IActionResult> MatchBouns([FromBody] int idMatch)
        {
            try
            {
                var response = await _matchInfoService.ShareBounusAfterMatchEnd(idMatch);
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
