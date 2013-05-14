namespace DDD.Bus
{
    public interface IEventHandler
    {
        void Handle<T>(T @event);
    }
}