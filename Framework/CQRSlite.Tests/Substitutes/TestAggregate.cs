using System;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate() { }
        public TestAggregate(Guid id)
        {
            Id = id;
            ApplyChange(new TestAggregateCreated());
        }

        public int DidSomethingCount;

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }

        public void DoSomethingElse()
        {
            ApplyChange(new TestAggregateDidSomeethingElse());
        }

        public override void Apply(IEvent @event)
        {
            if (@event.GetType() == typeof(TestAggregateDidSomething)) DidSomethingCount++;
        }
    }
}
