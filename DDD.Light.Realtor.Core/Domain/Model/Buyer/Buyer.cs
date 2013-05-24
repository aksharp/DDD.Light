using System;
using System.Collections.Generic;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Buyer;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer
{
    public class Buyer : Entity
    {
        private List<Property> _properties;
        private List<Guid> _offerIds;
        private Guid _prospectId;

        private Buyer()
        {
        }

        public Buyer(Guid id, Guid prospectId) : base(id)
        {
            _prospectId = prospectId;
            PublishEvent(new ProspectPromotedToBuyer(id, prospectId));
        }


        // API
        public void TakeOwnershipOf(Guid listingId)
        {
            // todo implement
            PublishEvent(new TookOwnershipOfListing());
        }

        public void NotifyOfAcceptedOffer(Guid offerId)
        {
            //todo implement
            PublishEvent(new NotifiedOfAcceptedOffer());
        }

        public void NotifyOfRejectedOffer(Offer.Offer offer)
        {
            throw new NotImplementedException();
        }

        public void PurchaseProperty()
        {
            throw new NotImplementedException();
        }

        public virtual void MakeAnOffer(Guid listingId, decimal price)
        {
            throw new NotImplementedException();
        }


        // Apply Events
        private void ApplyEvent(TookOwnershipOfListing @event)
        {
            throw new NotImplementedException();
        }
        
        private void ApplyEvent(NotifiedOfAcceptedOffer @event)
        {
            throw new NotImplementedException();
        }







    }
}