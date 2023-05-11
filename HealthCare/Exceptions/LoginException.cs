using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() { }

        public LoginException(string? message) : base(message) { }

        public LoginException(string? message, Exception? innerException) : base(message, innerException) { }

        protected LoginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
