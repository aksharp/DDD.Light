using DDD.Light.Realtor.Domain.Event.Offer;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Offer
{
    public class AcceptedHandler : EventHandler<Accepted>
    {
        public override void Handle(Accepted @event)
        {
            throw new System.NotImplementedException();
        }
    }
}