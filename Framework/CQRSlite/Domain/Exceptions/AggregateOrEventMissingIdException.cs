using System;
using System.Runtime.Serialization;

namespace CQRSlite.Domain.Exceptions
{
    [DataContract]
    public class AggregateOrEventMissingIdException : Exception
    {
        public AggregateOrEventMissingIdException()
        {
        }

        public AggregateOrEventMissingIdException(string message) : base(message)
        {
        }

        public AggregateOrEventMissingIdException(string message, Exception inner) : base(message, inner)
        {
        }

        public AggregateOrEventMissingIdException(Type aggregateType, Type eventType)
            : base(
                $"An event of type {eventType.FullName} was tried to save from {aggregateType.FullName} but no id where set on either"
                )
        {
        }
    }
}