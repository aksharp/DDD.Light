using System;
using System.Collections.Generic;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Realtor.Domain.Event.Realtor;

namespace DDD.Light.Realtor.Domain.Model.Realtor
{
    // aggregate root
    public class Realtor : Entity
    {
        private List<Guid> _offerIds; 
        private List<Guid> _listingIds; 

        private Realtor()
        {  
        }

        public Realtor(Guid id) : base(id)
        { 
            PublishEvent(new RealtorWasSetUp(id));
        }

        // API
        public void NotifyThatOfferWasMade(Guid offerId)
        {
            PublishEvent(new RealtorNotifiedThatOfferWasMade(offerId));
        }

        public void PostListing(Guid listingId)
        {
            PublishEvent(new RealtorPostedListing(listingId));
        }

        // Apply Events
        private void ApplyEvent(RealtorWasSetUp @event)
        {
            Id = @event.RealtorId;
        }
        
        private void ApplyEvent(RealtorPostedListing @event)
        {
            if (_listingIds == null)
                _listingIds = new List<Guid>();
            _listingIds.Add(@event.ListingId);
        }

        private void ApplyEvent(RealtorNotifiedThatOfferWasMade @event)
        {
            if (_offerIds == null)
                _offerIds = new List<Guid>();
            _offerIds.Add(@event.OfferId);
        }
    }
}