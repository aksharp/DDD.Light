namespace DDD.Light.Realtor.Core.Domain.Events.Listing
{
    public class Posted
    {
        public Model.Realtor.AggregateRoot.Realtor Realtor { get; set; }
        public Model.Listing.AggregateRoot.Listing Listing { get; set; }
    }
}