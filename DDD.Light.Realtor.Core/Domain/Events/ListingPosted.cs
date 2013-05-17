using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class ListingPosted
    {
        public Model.Realtor Realtor { get; set; }
        public Listing Listing { get; set; }
    }
}