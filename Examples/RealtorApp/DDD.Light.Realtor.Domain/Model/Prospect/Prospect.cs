using System;
using System.Collections.Generic;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Realtor.Domain.Event.Offer;

namespace DDD.Light.Realtor.Domain.Model.Prospect
{
    // aggregate root
    public class Prospect : AggregateRoot
    {
        private List<Guid> _offerIds;

        private Prospect()
        {            
        }

        public Prospect(Guid id) : base(id)
        {
            
        }

        // API
        public void MakeAnOffer(Guid offerId)
        {
            PublishAndApplyEvent(new OfferMade(offerId));
        }

        // Apply Events
        private void ApplyEvent(OfferMade @event)
        {
            if (_offerIds == null)
                _offerIds = new List<Guid>();
            _offerIds.Add(@event.OfferId);
        }

    }
}