using System;
using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model
{
    // aggregate root
    public class Realtor : Entity
    {
        public Realtor()
        {
            Offers = new List<Guid>();
            Listings = new List<Guid>();
        }

        public List<Guid> Listings { get; set; }
        public List<Guid> Offers { get; set; }

        public void NotifyThatOfferWasMade(Guid offerId)
        {
            Offers.Add(offerId);
        }

        public void PostListing(Listing listing)
        {
            Listings.Add(listing.Id);
            EventBus.Instance.Publish(new ListingPosted{Realtor = this, Listing = listing});
        }
    }
}