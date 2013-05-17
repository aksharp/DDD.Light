namespace DDD.Light.Messaging
{
    public abstract class EventHandler<T> : IEventHandler<T>, IHandler
    {
        public abstract void Handle(T @event);
        public void Subscribe()
        {
            EventBus.Instance.Subscribe(this);
        }
    }
}