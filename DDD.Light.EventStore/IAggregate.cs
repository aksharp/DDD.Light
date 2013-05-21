namespace DDD.Light.EventStore
{
    public interface IAggregate
    {
        void ApplyEvent<T>(T @event);
    }
}