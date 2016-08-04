using System;

namespace CQRSlite.Events
{
    public abstract class Event : IEvent
    {
        /// <summary>
        ///     AggregateRootId
        /// </summary>
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}