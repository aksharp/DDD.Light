using DDD.Light.Realtor.Domain.Event.Buyer;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Buyer
{
    public class TookOwnershipOfListingHandler : EventHandler<TookOwnershipOfListing>
    {
        public override void Handle(TookOwnershipOfListing @event)
        {
            throw new System.NotImplementedException();
        }
    }
}