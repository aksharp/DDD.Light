using DDD.Light.Realtor.Domain.Event.Offer;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Offer
{
    public class OfferMadeHandler : EventHandler<OfferMade>
    {
        public override void Handle(OfferMade @event)
        {
            throw new System.NotImplementedException();
        }
    }
}