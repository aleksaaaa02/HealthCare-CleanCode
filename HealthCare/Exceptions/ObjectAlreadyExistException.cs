using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Exceptions
{
    class ObjectAlreadyExistException : Exception
    {
        public ObjectAlreadyExistException()
        {
        }

        public ObjectAlreadyExistException(string? message) : base(message)
        {
        }

        public ObjectAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ObjectAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
