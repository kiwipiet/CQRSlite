using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSlite.Events
{
    public interface IEventStore
    {
        void Save(IEnumerable<IEvent> events);
        IEnumerable<IEvent> Get(Guid aggregateId, int fromVersion);
    }
    public interface IAsyncEventStore
    {
        Task SaveAsync(IEnumerable<IEvent> events);
        Task<IEnumerable<IEvent>> GetAsync(Guid aggregateId, int fromVersion);
    }
}