using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models
{
    // Source: https://github.com/nbarbettini/BeautifulRestApi/blob/master/src/Models/ApiError.cs
    public sealed class ApiError
    {
        public ApiError()
        {
        }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(Exception exception)
        {
            Message = exception.Message;
        }

        public string Message { get; set; }
    }
}
