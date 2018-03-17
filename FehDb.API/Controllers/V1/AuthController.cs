using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public AuthController(IConfiguration Configuration)
        {
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
        public IActionResult GetToken([Required, FromQuery]string user, [Required, FromQuery]string password)
        {
            if (user == null || password == null) return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            if (user != _configuration["Jwt:adminUser"] || password != _configuration["Jwt:adminPassword"])
                return Forbid("Incorrect username or password.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:adminUser"]),
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
    }

    public class JWTToken
    {
        public string Token { get; set; }
    }
}