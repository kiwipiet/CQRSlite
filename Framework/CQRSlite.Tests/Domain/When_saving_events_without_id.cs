﻿using System;
using CQRSlite.Domain;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Tests.Substitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.Domain
{
    [TestFixture]
    public class When_saving_events_without_id
    {
        private TestInMemoryEventStore _eventStore;
        private TestAggregate _aggregate;
        private Repository _rep;

        [SetUp]
        public void Setup()
        {
            _eventStore = new TestInMemoryEventStore();
            _rep = new Repository(_eventStore);

            _aggregate = new TestAggregate(Guid.Empty);
        }

        [Test]
        public void Should_throw_aggregate_or_event_missing_id_exception_from_repository()
        {
            Assert.Throws<AggregateOrEventMissingIdException>(() => _rep.Save(_aggregate, 0));
        }
    }
}