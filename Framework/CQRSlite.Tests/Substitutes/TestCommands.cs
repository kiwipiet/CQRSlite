using System;
using CQRSlite.Commands;

namespace CQRSlite.Tests.Substitutes
{
    public class TestAggregateDoSomething : ICommand
    {
        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }
    }

    public class TestAggregateDoSomethingHandler : ICommandHandler<TestAggregateDoSomething>
    {
        public int TimesRun { get; set; }

        public void Handle(TestAggregateDoSomething message)
        {
            TimesRun++;
        }
    }

    public class TestAggregateDoSomethingElseHandler : ICommandHandler<TestAggregateDoSomething>
    {
        public void Handle(TestAggregateDoSomething message)
        {
        }
    }
}