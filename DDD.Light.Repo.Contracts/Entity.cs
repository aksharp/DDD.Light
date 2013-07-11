namespace DDD.Light.Repo.Contracts
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }       

        protected Entity()
        {
            
        }

        protected Entity(TId id)
        {
            Id = id;
        }
    }
}