using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Domain.Event.Buyer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Buyer
{
    public class NotifiedOfAcceptedOfferHandler : EventHandler<NotifiedOfAcceptedOffer>
    {

        public override void Handle(NotifiedOfAcceptedOffer @event)
        {
        }
    }
}