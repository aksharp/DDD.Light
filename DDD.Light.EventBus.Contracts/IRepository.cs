namespace DDD.Light.EventBus.Contracts
{
    public interface IRepository<TId, TAggregate>
    {
        TAggregate GetById(TId id);
    }
}
