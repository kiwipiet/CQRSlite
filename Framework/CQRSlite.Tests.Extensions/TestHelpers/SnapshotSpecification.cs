using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Events;
using CQRSlite.Snapshots;
using NUnit.Framework;


namespace CQRSlite.Tests.Extensions.TestHelpers
{
    [TestFixture]
    public abstract class SnapshotSpecification<TAggregate, THandler, TCommand> : Specification<TAggregate, THandler, TCommand>
        where TAggregate : AggregateRoot
        where THandler : class, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected Snapshot Snapshot { get; set; }

        [SetUp]
        public override void Run()
        {
            var eventpublisher = new SpecEventPublisher();
            var eventstorage = new SpecEventStorage(eventpublisher, Given().ToList());
            var snapshotstorage = new SpecSnapShotStorage(Snapshot);

            var snapshotStrategy = new DefaultSnapshotStrategy();
            var repository = new SnapshotRepository(snapshotstorage, snapshotStrategy, new Repository(eventstorage), eventstorage);
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

            Snapshot = snapshotstorage.Snapshot;
            PublishedEvents = eventpublisher.PublishedEvents;
            EventDescriptors = eventstorage.Events;
        }
    }
    internal class SpecSnapShotStorage : ISnapshotStore
    {
        public SpecSnapShotStorage(Snapshot snapshot)
        {
            Snapshot = snapshot;
        }

        public Snapshot Snapshot { get; set; }

        public Snapshot Get(Guid id)
        {
            return Snapshot;
        }

        public void Save(Snapshot snapshot)
        {
            Snapshot = snapshot;
        }
    }

}
