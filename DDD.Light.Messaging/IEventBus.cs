namespace DDD.Light.Messaging
{
    public interface IEventBus
    {
        void Subscribe(IEventHandler handler);
        void Publish<T>(T @event);
    }
}