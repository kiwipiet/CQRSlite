﻿using CQRSlite.Domain.Exceptions;
using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CQRSlite.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes = new List<IEvent>();
        private readonly IDictionary<Type, Action<IEvent>> _applyEvent = new Dictionary<Type, Action<IEvent>>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        public IEnumerable<IEvent> FlushUncommitedChanges()
        {
            lock (_changes)
            {
                var changes = _changes.ToArray();
                var i = 0;
                foreach (var @event in changes)
                {
                    if (@event.Id == Guid.Empty && Id == Guid.Empty)
                    {
                        throw new AggregateOrEventMissingIdException(GetType(), @event.GetType());
                    }
                    if (@event.Id == Guid.Empty)
                    {
                        @event.Id = Id;
                    }
                    i++;
                    @event.Version = Version + i;
                    @event.TimeStamp = DateTimeOffset.UtcNow;
                }
                Version = Version + _changes.Count;
                _changes.Clear();
                return changes;
            }
        }

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history)
            {
                if (e.Version != Version + 1)
                {
                    throw new EventsOutOfOrderException(e.Id);
                }
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(IEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent @event, bool isNew)
        {
            lock (_changes)
            {
                Apply(@event);
                if (isNew)
                {
                    _changes.Add(@event);
                }
                else
                {
                    Id = @event.Id;
                    Version++;
                }
            }
        }

        public void Apply(IEvent @event)
        {
            Debug.WriteLine(@event);
            var eventType = @event.GetType();
            if (_applyEvent.ContainsKey(eventType)) _applyEvent[eventType].Invoke(@event);
        }

        public void AddEventAction<T>(Action<IEvent> action) where T : IEvent
        {
            _applyEvent.Add(typeof(T), action);
        }

        public void SyncEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event);
            }
        }
    }
}