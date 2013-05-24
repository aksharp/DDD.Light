using System;
using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Listing;

namespace DDD.Light.Realtor.Core.Domain.Model.Realtor.AggregateRoot
{
    // aggregate root
    public class Realtor : Entity
    {
        private readonly IList<Guid> _offerIds;
        private readonly IList<Guid> _listingIds;

        public Realtor(Guid id, IList<Guid> offerIds, IList<Guid> listingIds) : base(id)
        {
            _offerIds = offerIds ?? new List<Guid>();
            _listingIds = listingIds ?? new List<Guid>();
        }


        public void NotifyThatOfferWasMade(Guid offerId)
        {
            _offerIds.Add(offerId);
        }

        public void PostListing(Listing.AggregateRoot.Listing listing)
        {
            _listingIds.Add(listing.Id);
            EventBus.Instance.Publish(new Posted{Realtor = this, Listing = listing});
        }
    }
}