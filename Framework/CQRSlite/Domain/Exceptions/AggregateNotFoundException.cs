using System;
using System.Runtime.Serialization;

namespace CQRSlite.Domain.Exceptions
{
    [DataContract]
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message) : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public AggregateNotFoundException(Type t, Guid id)
            : base($"Aggregate {id} of type {t.FullName} was not found")
        {
        }
    }
}