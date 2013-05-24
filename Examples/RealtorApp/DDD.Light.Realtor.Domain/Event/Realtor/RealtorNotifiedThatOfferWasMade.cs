using System;

namespace DDD.Light.Realtor.Domain.Event.Realtor
{
    public class RealtorNotifiedThatOfferWasMade
    {
        public Guid OfferId { get; private set; }

        public RealtorNotifiedThatOfferWasMade(Guid offerId)
        {
            OfferId = offerId;
        }
    }
}