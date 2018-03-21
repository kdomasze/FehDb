using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FehDb.API.Models.Resource.UserModel;
using FehDb.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FehDb.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/V1/Auth")]
    public class AuthController : Controller
    {
        private IConfiguration _configuration;
        private readonly IAuthService _service;

        public AuthController(IAuthService service, IConfiguration Configuration)
        {
            _service = service;
            _configuration = Configuration;
        }

        /// <summary>
        /// Returns a JWT token for admin actions
        /// </summary>
        /// <param name="user">The username of the administrator (default: User)</param>
        /// <param name="password">The password of the administrator (default: Password)</param>
        /// <returns>A JWT token</returns>
        /// <response code="200">Returns the JWT Token</response>
        /// <response code="400">Username or password is null</response>
        /// <response code="403">Incorrect username or password</response>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Token")]
        [ProducesResponseType(typeof(JWTToken), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        public async Task<IActionResult> GetToken([Required, FromQuery]string user, [Required, FromQuery]string password)
        {
            if (user == null || password == null) return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            Models.Entity.UserModel.User userAccount;

            try
            {
                userAccount = await _service.CheckIfValidAccount(user, password);
            }
            catch(Exception e)
            {
                return Forbid(e.Message);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userAccount.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], //issued by
                _configuration["Jwt:Audience"], //issued for
                claims, //payload
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireTime"])), // valid for 1/2 hour
                signingCredentials: creds); // signature

            var tokenEncoded = new JwtSecurityTokenHandler().WriteToken(token);

            var result = new JWTToken()
            {
                Token = tokenEncoded
            };

            return new OkObjectResult(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([Required, FromBody]User user)
        {
            if (user.Username == null || user.Password == null) return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            if (!(await _service.CreateAccount(user.Username, user.Password))) return BadRequest();

            return new NoContentResult();
        }
    }
}

public class JWTToken
{
    public string Token { get; set; }
}