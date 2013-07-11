using DDD.Light.EventBus.Contracts;

namespace DDD.Light.EventBus.InProcess
{
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>, IHandler
    {
        public abstract void Handle(TEvent @event);
        public void Subscribe()
        {
            EventBus.Instance.Subscribe(this);
        }
    }
}