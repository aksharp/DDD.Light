using System;

namespace DDD.Light.Realtor.Domain.Event.Realtor
{
    public class RealtorAddedNewListing
    {
        public Guid ListingId { get; private set; }

        public RealtorAddedNewListing(Guid listingId)
        {
            ListingId = listingId;
        }
    }
}