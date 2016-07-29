using System;
using System.Threading.Tasks;

namespace CQRSlite.Domain
{
    public interface IRepository
    {
        void Save<T>(T aggregate, int? expectedVersion = null) where T : AggregateRoot;
        T Get<T>(Guid aggregateId) where T : AggregateRoot;
    }
    public interface IAsyncRepository
    {
        Task<T> SaveAsync<T>(T aggregate, int? expectedVersion = null) where T : AggregateRoot;
        Task<T> GetAsync<T>(Guid aggregateId) where T : AggregateRoot;
    }
}