using CQRSlite.Messages;
using System;

namespace CQRSlite.Events
{
    public interface IEvent : IMessage
    {
        /// <summary>
        /// AggregateRootId
        /// CorrelationId
        /// </summary>
        Guid Id { get; set; }
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}

