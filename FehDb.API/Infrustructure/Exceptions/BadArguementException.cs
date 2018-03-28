using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Exceptions
{
    public class BadArguementException : Exception
    {
        public BadArguementException()
        {
        }

        public BadArguementException(string message) : base(String.Format("Supplied argument invalid: {0}", message))
        {
        }

        public BadArguementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
