﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Exceptions
{
    internal class ValidationException : Exception
    {
        public ValidationException() : this("Uneseni neispravni podaci. Pokušajte ponovo.") { }

        public ValidationException(string? message) : base(message) { }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException) { }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
