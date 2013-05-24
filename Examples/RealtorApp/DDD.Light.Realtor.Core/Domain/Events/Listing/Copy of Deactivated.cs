using System;
using DDD.Light.Realtor.Core.Domain.Model.Listing;

namespace DDD.Light.Realtor.Core.Domain.Events.Listing
{
    public class Described
    {
        public Guid ListingId { get; private set; }
        public Description Description { get; private set; }

        public Described(Guid listingId, Description description)
        {
            ListingId = listingId;
            Description = description;
        }
    }
}