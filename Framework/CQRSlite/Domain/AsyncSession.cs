using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRSlite.Domain.Exceptions;

namespace CQRSlite.Domain
{
    public class AsyncSession : IAsyncSession
    {
        private readonly IAsyncRepository _repository;
        private readonly Dictionary<Guid, AggregateDescriptor> _trackedAggregates;

        public AsyncSession(IAsyncRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
            _trackedAggregates = new Dictionary<Guid, AggregateDescriptor>();
        }

        public Task AddAsync<T>(T aggregate) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Id))
            {
                _trackedAggregates.Add(aggregate.Id, new AggregateDescriptor { Aggregate = aggregate, Version = aggregate.Version });
            }
            else if (_trackedAggregates[aggregate.Id].Aggregate != aggregate)
            {
                throw new ConcurrencyException(aggregate.Id);
            }
            return Task.FromResult(0);
        }

        public async Task<T> GetAsync<T>(Guid id, int? expectedVersion = null) where T : AggregateRoot
        {
            if (IsTracked(id))
            {
                var trackedAggregate = (T)_trackedAggregates[id].Aggregate;
                if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
                {
                    throw new ConcurrencyException(trackedAggregate.Id);
                }
                return trackedAggregate;
            }

            var aggregate = await _repository.GetAsync<T>(id);
            if (expectedVersion != null && aggregate.Version != expectedVersion)
            {
                throw new ConcurrencyException(id);
            }
            await AddAsync(aggregate);

            return aggregate;
        }

        private bool IsTracked(Guid id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public async Task CommitAsync()
        {
            foreach (var descriptor in _trackedAggregates.Values)
            {
                await _repository.SaveAsync(descriptor.Aggregate, descriptor.Version);
            }
            _trackedAggregates.Clear();
        }
    }
}