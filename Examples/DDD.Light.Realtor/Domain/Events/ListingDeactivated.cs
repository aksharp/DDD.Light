using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class ListingDeactivated
    {
        public Listing Listing { get; set; }
    }
}