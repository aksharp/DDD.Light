namespace DDD.Light.Messaging
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler);
        void Publish<T>(T @event);
    }
}