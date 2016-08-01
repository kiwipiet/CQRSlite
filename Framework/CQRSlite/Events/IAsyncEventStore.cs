using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSlite.Events
{
    public interface IAsyncEventStore
    {
        Task SaveAsync(IEnumerable<IEvent> events);
        Task<IEnumerable<IEvent>> GetAsync(Guid aggregateId, int fromVersion);
    }
}