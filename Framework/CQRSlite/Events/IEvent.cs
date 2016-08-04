using System;
using CQRSlite.Messages;

namespace CQRSlite.Events
{
    public interface IEvent : IMessage
    {
        /// <summary>
        ///     AggregateRootId
        /// </summary>
        Guid Id { get; set; }

        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}