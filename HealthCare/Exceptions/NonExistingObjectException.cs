using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Exceptions
{
    internal class NonExistingObjectException : Exception
    {
        public NonExistingObjectException()
        {
        }

        public NonExistingObjectException(string? message) : base(message)
        {
        }

        public NonExistingObjectException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NonExistingObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
