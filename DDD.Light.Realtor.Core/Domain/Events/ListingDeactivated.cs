using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class ListingDeactivated
    {
        public Listing Listing { get; set; }
    }
}