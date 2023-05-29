﻿using HealthCare.Exceptions;
using HealthCare.Model;

namespace HealthCare.Application
{
    public static class Context
    {
        private static User? _current = null;
        public static User Current
        {
            get => _current is not null ?
                _current : throw new LoginException("Korisnik nije ulogovan.");
            set => _current = value;
        }

        public static void Reset()
        {
            _current = null;
        }
    }
}
