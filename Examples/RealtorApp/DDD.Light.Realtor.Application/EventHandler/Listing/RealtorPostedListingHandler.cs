using DDD.Light.EventStore.Contracts;
using DDD.Light.Realtor.Domain.Event.Realtor;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class RealtorPostedListingHandler : EventHandler<RealtorPostedListing>
    {
        private readonly IEventStore _eventStore;

        public RealtorPostedListingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override void Handle(RealtorPostedListing @event)
        {
            var listing = _eventStore.GetById<Domain.Model.Listing.Listing>(@event.ListingId);
            listing.Post();
        }
    }
}