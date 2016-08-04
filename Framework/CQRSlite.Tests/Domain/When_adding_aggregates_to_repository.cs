using System;
using CQRSlite.Domain;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Tests.Substitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.Domain
{
    [TestFixture]
    public class When_adding_aggregates_to_repository
    {
        [SetUp]
        public void SetUp()
        {
            var eventStore = new TestInMemoryEventStore();
            _session = new Session(new Repository(eventStore));
        }

        private Session _session;

        [Test]
        public void Should_not_throw_if_object_already_tracked()
        {
            var aggregate = new TestAggregate(Guid.NewGuid());
            _session.Add(aggregate);
            _session.Add(aggregate);
        }

        [Test]
        public void Should_throw_if_different_object_with_tracked_guid_is_added()
        {
            var aggregate = new TestAggregate(Guid.NewGuid());
            var aggregate2 = new TestAggregate(aggregate.Id);
            _session.Add(aggregate);
            Assert.Throws<ConcurrencyException>(() => _session.Add(aggregate2));
        }
    }
}