namespace DDD.Light.Repo.Contracts
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}