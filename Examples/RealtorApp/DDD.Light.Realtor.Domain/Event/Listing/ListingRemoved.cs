using System;

namespace DDD.Light.Realtor.Domain.Event.Listing
{
    public class ListingRemoved
    {
        public Guid ListingId { get; private set; }

        public ListingRemoved(Guid listingId)
        {
            ListingId = listingId;
        }
    }
}