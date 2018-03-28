using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FehDb.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FehDb.API.Controllers.V1
{
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [Route("api/v1/Error")]
        public IActionResult Index()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(exception != null)
            {
                Exception e = exception.Error;

                return BadRequest(new ApiError(e));
            }

            return NotFound(new ApiError("An unknown error has occured."));
        }
    }
}