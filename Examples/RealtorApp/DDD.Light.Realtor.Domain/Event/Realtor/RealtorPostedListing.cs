using System;

namespace DDD.Light.Realtor.Domain.Event.Realtor
{
    public class RealtorPostedListing
    {
        public Guid ListingId { get; private set; }

        public RealtorPostedListing(Guid listingId)
        {
            ListingId = listingId;
        }
    }
}