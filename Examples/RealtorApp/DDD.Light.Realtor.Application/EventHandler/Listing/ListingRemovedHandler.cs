using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Realtor.Domain.Event.Listing;
using DDD.Light.Repo.Contracts;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class ListingRemovedHandler : EventHandler<ListingRemoved>
    {
        private readonly IRepository<ActiveListing> _activeListings;

        public ListingRemovedHandler(IRepository<ActiveListing> activeListings)
        {
            _activeListings = activeListings;
        }

        public override void Handle(ListingRemoved @event)
        {
            _activeListings.Delete(@event.ListingId);
        }
    }
}