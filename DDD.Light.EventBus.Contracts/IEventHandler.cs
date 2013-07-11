namespace DDD.Light.EventBus.Contracts
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}