namespace DDD.Light.Messaging
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}