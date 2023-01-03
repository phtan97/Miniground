using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniGround.API.ContextModels.Tables;
using MiniGround.API.Dependency.Interfaces;
using MiniGround.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniGround.API.Controllers
{
    [Route("api/authens")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthenticationsController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginModel userLogin)
        {
            try
            {
                var response = await _userService.Login(userLogin);
                if(response.Code == Errors.SUCCESS.Code)
                {
                    dynamic user = response.Data;
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        user
                    });
                }
                return Unauthorized(response.Data);
            }
            catch(Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "server Errors");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegister)
        {
            try
            {
                var response = await _userService.Register(userRegister);
                if (response.Code == Errors.SUCCESS.Code)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Data);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "server Errors");
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(string username)
        {
            try
            {

            }
            catch(Exception ex)
            {
                _Log.Error(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "server Errors");
            }
        }
    }
}
