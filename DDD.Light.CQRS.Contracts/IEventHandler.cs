namespace DDD.Light.CQRS.Contracts
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}