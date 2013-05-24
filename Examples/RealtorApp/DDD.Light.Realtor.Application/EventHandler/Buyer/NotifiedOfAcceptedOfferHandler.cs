using DDD.Light.Realtor.Domain.Event.Buyer;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Buyer
{
    public class NotifiedOfAcceptedOfferHandler : EventHandler<NotifiedOfAcceptedOffer>
    {

        public override void Handle(NotifiedOfAcceptedOffer @event)
        {
        }
    }
}