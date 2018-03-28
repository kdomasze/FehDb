using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Auth.Exceptions
{
    public class UserLoginTimeoutException : Exception
    {
        public UserLoginTimeoutException()
        {
        }

        public UserLoginTimeoutException(string message) : base(String.Format("User must wait before attempting to login: {0}", message))
        {
        }

        public UserLoginTimeoutException(string message, Exception innerException) : base(String.Format("User must wait before attempting to login: {0}", message), innerException)
        {
        }
    }
}
