using System;
using System.Collections.Generic;
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

        [Route("Token")]
        [HttpGet]
        public IActionResult GetToken([FromQuery]string user, [FromQuery]string password)
        {
            if (user != _configuration["Jwt:adminUser"] || password != _configuration["Jwt:adminPassword"])
                return Forbid();

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

            return new OkObjectResult(new { token = tokenEncoded });
        }
    }
}