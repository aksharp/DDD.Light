namespace DDD.Bus
{
    public interface IEventBus
    {
        void Subscribe(IEventHandler handler);
        void Publish<T>(T @event);
    }
}