namespace DDD.Light.Realtor.Core.Domain.Events.Listing
{
    public class Deactivated
    {
        public Model.Listing.AggregateRoot.Listing Listing { get; set; }
    }
}