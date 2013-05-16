using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Realtor : Entity
    {
        public Realtor()
        {
            Offers = new List<Guid>();
        }

        public IEnumerable<Guid> Listings { get; set; }
        public IEnumerable<Guid> Offers { get; set; }

        public void NotifyThatOfferWasMade(Guid offerId)
        {
            Offers.ToList().Add(offerId);
        }

        public void PostListing(Listing listing)
        {
            Listings.ToList().Add(listing.Id);
            EventBus.Instance.Publish(new ListingPosted{Realtor = this, Listing = listing});
        }
    }
}