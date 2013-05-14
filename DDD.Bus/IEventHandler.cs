namespace DDD.Light.Messaging
{
    public interface IEventHandler
    {
        void Handle<T>(T @event);
    }
}