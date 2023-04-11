using System;
using System.Runtime.Serialization;

namespace HealthCare.Exceptions
{
    [Serializable]
    internal class NotEnoughEquipmentException : Exception
    {
        public NotEnoughEquipmentException()
        {
        }

        public NotEnoughEquipmentException(string? message) : base(message)
        {
        }

        public NotEnoughEquipmentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotEnoughEquipmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}