using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FehDb.API.Models.Entity.UserModel;
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
        /// <param name="user">The login details of the user</param>
        /// <returns>A JWT token</returns>
        /// <response code="200">Returns the JWT Token</response>
        /// <response code="400">Username or password is null</response>
        /// <response code="403">Incorrect username or password</response>
        [HttpGet("Token")]
        [ProducesResponseType(typeof(JWTToken), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        public async Task<IActionResult> GetToken([Required, FromQuery]UserResource user)
        {
            if (user.Username == null || user.Password == null)
                return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            User userAccount;

            try
            {
                userAccount = await _service.CheckIfValidAccount(user);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            var result = _service.GenerateJwtToken(userAccount);

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <remarks>
        /// There is no need to make an account for accessing data. An account is only necessary for updating data.
        /// </remarks>
        /// <param name="user">The user credentials</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully created user</response>
        /// <response code="400">Username or password is null</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<IActionResult> CreateAccount([Required, FromBody]UserResource user)
        {
            if (user.Username == null || user.Password == null)
                return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            await _service.CreateAccount(user);

            return new NoContentResult();
        }

        /// <summary>
        /// Changes the password of the user specified
        /// </summary>
        /// <param name="user">The user credentials along with the new password</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully updated user password</response>
        /// <response code="400">Bad request</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<IActionResult> ChangePassword([Required, FromBody]UserPasswordChangeResource user)
        {
            if (user.Username == null || user.Password == null || user.NewPassword == null)
                return BadRequest(new ArgumentNullException("The supplied username or password is null."));

            try
            {
                await _service.ChangePassword(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return new NoContentResult();
        }
    }
}

public class JWTToken
{
    public string Token { get; set; }
}