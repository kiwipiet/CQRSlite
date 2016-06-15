using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestInMemoryEventStore : IEventStore
    {
        private readonly IEventPublisher _eventPublisher;
        public readonly List<IEvent> Events = new List<IEvent>();

        public TestInMemoryEventStore()
        {
        }
        public TestInMemoryEventStore(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Save<T>(IEnumerable<IEvent> events)
        {
            lock (Events)
            {
                Events.AddRange(events);
                if (_eventPublisher != null)
                {
                    foreach (var @event in Events)
                    {
                        _eventPublisher.Publish(@event);
                    }
                }
            }
        }

        public IEnumerable<IEvent> Get<T>(Guid aggregateId, int fromVersion)
        {
            lock (Events)
            {
                return Events.Where(x => x.Version > fromVersion && x.Id == aggregateId).OrderBy(x => x.Version).ToList();
            }
        }
    }
}