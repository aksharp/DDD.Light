namespace DDD.Light.EventBus.Contracts
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }

}
