namespace DDD.Light.Realtor.Core.Domain.Events.Listing
{
    public class Posted
    {
        public Model.Realtor.Realtor Realtor { get; set; }
        public Model.Listing.Listing Listing { get; set; }
    }
}