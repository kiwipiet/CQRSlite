using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestEventPublisher : IEventPublisher
    {
        public int Published { get; private set; }

        public void Publish<T>(T @event) where T : IEvent
        {
            Published++;
        }
    }
}