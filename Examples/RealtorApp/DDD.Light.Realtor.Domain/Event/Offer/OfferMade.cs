using System;

namespace DDD.Light.Realtor.Domain.Event.Offer
{
    public class OfferMade
    {
        public Guid OfferId { get; private set; }

        public OfferMade(Guid offerId)
        {
            OfferId = offerId;
        }
    }
}