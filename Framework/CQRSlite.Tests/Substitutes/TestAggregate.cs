using System;
using CQRSlite.Domain;

namespace CQRSlite.Tests.Substitutes
{
    public class TestAggregate : AggregateRoot
    {
        public int DidSomethingCount;

        public TestAggregate()
        {
            AddEventAction<TestAggregateDidSomething>(e => DidSomethingCount++);
        }

        public TestAggregate(Guid id) : this()
        {
            Id = id;
            ApplyChange(new TestAggregateCreated());
        }

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }

        public void DoSomethingElse()
        {
            ApplyChange(new TestAggregateDidSomeethingElse());
        }
    }
}