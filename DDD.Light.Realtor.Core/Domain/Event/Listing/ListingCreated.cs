﻿namespace DDD.Light.Realtor.Core.Domain.Event.Listing
{
    public class ListingCreated
    {
        public Model.Listing.Listing Listing { get; private set; }

        public ListingCreated(Model.Listing.Listing listing)
        {
            Listing = listing;
        }
    }
}