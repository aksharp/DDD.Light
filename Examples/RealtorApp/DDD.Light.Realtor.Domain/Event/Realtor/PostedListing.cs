using System;

namespace DDD.Light.Realtor.Domain.Event.Realtor
{
    public class PostedListing
    {
        public Guid ListingId { get; private set; }

        public PostedListing(Guid listingId)
        {
            ListingId = listingId;
            //todo: implement
        }
    }
}