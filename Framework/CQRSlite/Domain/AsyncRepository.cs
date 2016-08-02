using System;
using System.Linq;
using System.Threading.Tasks;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Domain.Factories;
using CQRSlite.Events;

namespace CQRSlite.Domain
{
    public class AsyncRepository : IAsyncRepository
    {
        private readonly IAsyncEventStore _eventStore;

        public AsyncRepository(IAsyncEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException(nameof(eventStore));
            }

            _eventStore = eventStore;
        }

        public async Task<T> SaveAsync<T>(T aggregate, int? expectedVersion = null) where T : AggregateRoot
        {
            if (expectedVersion != null)
            {
                var x = await _eventStore.GetAsync(aggregate.Id, expectedVersion.Value);
                if (x.Any())
                {
                    throw new ConcurrencyException(aggregate.Id);
                }
            }

            var changes = aggregate.FlushUncommitedChanges();
            await _eventStore.SaveAsync(changes);
            return aggregate;
        }

        public async Task<T> GetAsync<T>(Guid aggregateId) where T : AggregateRoot
        {
            var eventsTask = await _eventStore.GetAsync(aggregateId, -1);
            var events = eventsTask.ToList();
            if (!events.Any())
            {
                throw new AggregateNotFoundException(typeof(T), aggregateId);
            }

            var aggregate = AggregateFactory.CreateAggregate<T>();
            aggregate.LoadFromHistory(events);
            return aggregate;
        }
    }
}