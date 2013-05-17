namespace DDD.Light.Messaging
{
    public abstract class EventHandler<T> : IEventHandler<T>
    {
        public abstract void Handle(T @event);
        public void Subscribe(IEventBus eventBus)
        {
            eventBus.Subscribe(this);
        }
    }
}