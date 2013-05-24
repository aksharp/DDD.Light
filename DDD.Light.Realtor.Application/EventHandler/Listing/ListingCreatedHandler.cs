using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Listing;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class ListingCreatedHandler : EventHandler<ListingCreated>
    {
        private readonly IRepository<Core.Domain.Model.Listing.Listing> _listings;

        public ListingCreatedHandler(IRepository<Core.Domain.Model.Listing.Listing> listings)
        {
            _listings = listings;
        }

        public override void Handle(ListingCreated @event)
        {
            _listings.Save(@event.Listing);
        }
    }
}