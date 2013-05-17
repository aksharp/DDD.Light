using System;
using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Listing;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model.Realtor.AggregateRoot
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

        public void PostListing(Listing.AggregateRoot.Listing listing)
        {
            Listings.Add(listing.Id);
            EventBus.Instance.Publish(new Posted{Realtor = this, Listing = listing});
        }
    }
}