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
    [Route("api/fieldprices")]
    [ApiController]
    public class FieldPricesController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFieldPriceService _fieldPriceService;
        public FieldPricesController(IFieldPriceService fieldPriceService)
        {
            _fieldPriceService = fieldPriceService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetFieldFootballPrice(int fieldFootballId)
        {
            try
            {
                var response = await _fieldPriceService.GetFieldPriceByFootBallField(fieldFootballId);
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

        [HttpPut]
        [Route("update/price")]
        public async Task<IActionResult> UpdateFieldPriceFootball(int id, double price)
        {
            try
            {
                var response = await _fieldPriceService.UpdateFiedPriceByFootBallField(id, price);
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

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertFieldPriceFootball([FromBody] InsertFieldPriceFootballModel fieldPriceFootballModel)
        {
            try
            {
                var response = await _fieldPriceService.InsertFiedPriceByFootBallField(fieldPriceFootballModel.IdFootballField, fieldPriceFootballModel.StartDate, fieldPriceFootballModel.EndDate, fieldPriceFootballModel.Price);
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
