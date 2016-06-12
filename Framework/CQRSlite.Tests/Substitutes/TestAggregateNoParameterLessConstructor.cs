using System;
using CQRSlite.Domain;
using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestAggregateNoParameterLessConstructor : AggregateRoot
    {
        public TestAggregateNoParameterLessConstructor(int i, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }

        public override void Apply(IEvent @event)
        {
        }
    }
}