using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Events;
using NUnit.Framework;

namespace CQRSlite.Tests.Extensions.TestHelpers
{
	[TestFixture]
    public abstract class Specification<TAggregate, THandler, TCommand> 
        where TAggregate: AggregateRoot
        where THandler : class, ICommandHandler<TCommand>
        where TCommand : ICommand
    {

        protected TAggregate Aggregate { get; set; }
        protected ISession Session { get; set; }
        protected abstract IEnumerable<IEvent> Given();
        protected abstract TCommand When();
        protected abstract THandler BuildHandler();

        protected IList<IEvent> EventDescriptors { get; set; }
        protected IList<IEvent> PublishedEvents { get; set; }
		
        [SetUp]
        public virtual void Run()
        {
            var eventpublisher = new SpecEventPublisher();
            var eventstorage = new SpecEventStorage(eventpublisher, Given().ToList());
            var repository = new Repository(eventstorage);
            Session = new Session(repository);

            try
            {
                Aggregate = Session.Get<TAggregate>(Guid.Empty);
            }
            catch (AggregateNotFoundException)
            {
		        
            }

            var handler = BuildHandler();
            handler.Handle(When());

            PublishedEvents = eventpublisher.PublishedEvents;
            EventDescriptors = eventstorage.Events;
        }
    }

    internal class SpecEventPublisher : IEventPublisher
    {
        public SpecEventPublisher()
        {
            PublishedEvents = new List<IEvent>();
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            PublishedEvents.Add(@event);
        }

        public IList<IEvent> PublishedEvents { get; set; }
    }

    internal class SpecEventStorage : IEventStore
    {
        private readonly IEventPublisher _publisher;

        public SpecEventStorage(IEventPublisher publisher, List<IEvent> events)
        {
            _publisher = publisher;
            Events = events;
        }

        public List<IEvent> Events { get; set; }

        public void Save(IEnumerable<IEvent> events)
        {
            Events.AddRange(events);
            foreach (var @event in events)
                _publisher.Publish(@event);
        }

        public IEnumerable<IEvent> Get(Guid aggregateId, int fromVersion)
        {
            return Events.Where(x => x.Version > fromVersion);
        }
    }
}
