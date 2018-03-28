using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Auth.Exceptions
{
    [Serializable]
    public class WrongUserCredentialsException : Exception
    {
        public WrongUserCredentialsException()
        {
        }

        public WrongUserCredentialsException(string message) : base(String.Format("Invalid user credentials: {0}", message))
        {
        }

        public WrongUserCredentialsException(string message, Exception innerException) : base(String.Format("Invalid user credentials: {0}", message), innerException)
        {
        }
    }
}
