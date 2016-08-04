using System;
using System.Runtime.Serialization;

namespace CQRSlite.Domain.Exceptions
{
    [DataContract]
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
        {
        }

        public ConcurrencyException(string message) : base(message)
        {
        }

        public ConcurrencyException(string message, Exception inner) : base(message, inner)
        {
        }

        public ConcurrencyException(Guid id)
            : base($"A different version than expected was found in aggregate {id}")
        {
        }
    }
}