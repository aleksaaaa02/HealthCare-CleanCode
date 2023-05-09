﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Exceptions
{
    class UsernameNotFoundException : LoginException
    {
        public UsernameNotFoundException() : this("Nepostojeće korisnicko ime. Pokušajte ponovo") { }

        public UsernameNotFoundException(string? message) : base(message) { }

        public UsernameNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

        protected UsernameNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
