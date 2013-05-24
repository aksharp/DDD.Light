using System;
using System.Collections.Generic;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Realtor;

namespace DDD.Light.Realtor.Core.Domain.Model.Realtor
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

        public void PostListing(Listing.Listing listing)
        {
            PublishEvent(new PostedListing(listing));
        }

        private void ApplyEvent(PostedListing @event)
        {
            Listings.Add(@event.Listing.Id);
        }
    }
}