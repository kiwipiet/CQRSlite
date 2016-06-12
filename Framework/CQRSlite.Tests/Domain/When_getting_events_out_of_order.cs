﻿using System;
using CQRSlite.Domain;
using CQRSlite.Domain.Exceptions;
using CQRSlite.Tests.Substitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.Domain
{
	[TestFixture]
    public class When_getting_events_out_of_order
    {
	    private ISession _session;

	    [SetUp]
        public void Setup()
        {
            var eventStore = new TestEventStoreWithBugs();
            _session = new Session(new Repository(eventStore));
        }

        [Test]
        public void Should_throw_concurrency_exception()
        {
            var id = Guid.NewGuid();
            Assert.Throws<EventsOutOfOrderException>(() => _session.Get<TestAggregate>(id, 3));
        }
    }
}