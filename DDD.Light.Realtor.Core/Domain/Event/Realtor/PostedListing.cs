namespace DDD.Light.Realtor.Core.Domain.Event.Realtor
{
    public class PostedListing
    {
        public Model.Listing.Listing Listing { get; private set; }

        public PostedListing(Model.Listing.Listing listing)
        {
            Listing = listing;
        }
    }
}