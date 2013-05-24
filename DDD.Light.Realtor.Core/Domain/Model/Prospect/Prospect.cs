using System;
using System.Collections.Generic;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Offer;

namespace DDD.Light.Realtor.Core.Domain.Model.Prospect
{
    // aggregate root
    public class Prospect : Entity
    {
        private List<Guid> _offerIds;

        private Prospect()
        {            
        }

        public Prospect(Guid id) : base(id)
        {
            
        }

        // API
        public void MakeAnOffer(Offer.Offer offer)
        {
            PublishEvent(new OfferMade(offer));
        }

        // Apply Events
        private void ApplyEvent(OfferMade @event)
        {
            if (_offerIds == null)
                _offerIds = new List<Guid>();
            _offerIds.Add(@event.Offer.Id);
        }

    }
}