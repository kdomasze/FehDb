using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FehDb.API.Controllers.Test
{
    [Produces("application/json")]
    [Route("api/Test/Auth")]
    public class AuthController : Controller
    {
        [Route("GetToken")]
        [HttpGet]
        public IActionResult GetToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "TestUser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("!~YOUR.SECRET.HERE~!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://localhost:5001", //issued by
                "http://localhost:54359", //issued for
                claims, //payload
                expires: DateTime.Now.AddMinutes(30), // valid for 1/2 hour
                signingCredentials: creds); // signature

            var tokenEncoded = new JwtSecurityTokenHandler().WriteToken(token);

            return new OkObjectResult(new { token = tokenEncoded });
        }
    }
}