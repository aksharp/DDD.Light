namespace DDD.Light.Messaging.Contracts
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}