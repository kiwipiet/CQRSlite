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

        public ConcurrencyException(Guid id, int expectedVersion)
            : base($"Expected version {expectedVersion} in aggregate {id}")
        {
        }
        public ConcurrencyException(Guid id, int expectedVersion, int aggregateVersion)
            : base($"Expected version {expectedVersion} but found {aggregateVersion} in aggregate {id}")
        {
        }
    }
}