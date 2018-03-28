using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException()
        {
        }

        public InvalidModelException(string message) : base(String.Format("Supplied model invalid: {0}", message))
        {
        }

        public InvalidModelException(string property, string issue) : base(String.Format("Supplied model invalid: {0} - {1}", property, issue))
        {
        }

        public InvalidModelException(string message, Exception innerException) : base(String.Format("Supplied model invalid: {0}", message), innerException)
        {
        }
    }
}
