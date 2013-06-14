using System;
using System.Collections.Generic;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Realtor.Domain.Event.Realtor;

namespace DDD.Light.Realtor.Domain.Model.Realtor
{
    // aggregate root
    public class Realtor : AggregateRoot
    {
        private List<Guid> _offerIds; 
        private List<Guid> _postedListingIds; 
        private List<Guid> _newListingIds; 

        private Realtor()
        {  
        }

        public Realtor(Guid id) : base(id)
        {
//            Publish<Realtor, RealtorWasSetUp>(new RealtorWasSetUp(id));
            PublishEvent(new RealtorWasSetUp(id));
        }

        // API
        public void NotifyThatOfferWasMade(Guid offerId)
        {
            PublishEvent(new RealtorNotifiedThatOfferWasMade(offerId));
        }

        public void AddNewListing(Guid listingId)
        {
            PublishEvent(new RealtorAddedNewListing(listingId));
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

        private void ApplyEvent(RealtorAddedNewListing @event)
        {
            if (_newListingIds == null)
                _newListingIds = new List<Guid>();
            _newListingIds.Add(@event.ListingId);
        }
        
        private void ApplyEvent(RealtorPostedListing @event)
        {
            _newListingIds.Remove(@event.ListingId);
            if (_postedListingIds == null)
                _postedListingIds = new List<Guid>();
            _postedListingIds.Add(@event.ListingId);
        }

        private void ApplyEvent(RealtorNotifiedThatOfferWasMade @event)
        {
            if (_offerIds == null)
                _offerIds = new List<Guid>();
            _offerIds.Add(@event.OfferId);
        }
    }
}