using System;
using System.Runtime.Serialization;

namespace CQRSlite.Domain.Exceptions
{
    [DataContract]
    public class EventsOutOfOrderException : Exception
    {
        public EventsOutOfOrderException()
        {
        }

        public EventsOutOfOrderException(string message) : base(message)
        {
        }

        public EventsOutOfOrderException(string message, Exception inner) : base(message, inner)
        {
        }

        public EventsOutOfOrderException(Guid id)
            : base($"Eventstore gave event for aggregate {id} out of order")
        {
        }
    }
}