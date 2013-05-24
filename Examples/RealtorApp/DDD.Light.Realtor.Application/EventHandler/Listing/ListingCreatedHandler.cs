using DDD.Light.Realtor.Domain.Event.Listing;
using DDD.Light.Repo.Contracts;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class ListingCreatedHandler : EventHandler<ListingCreated>
    {
        private readonly IRepository<Domain.Model.Listing.Listing> _listings;

        public ListingCreatedHandler(IRepository<Domain.Model.Listing.Listing> listings)
        {
            _listings = listings;
        }

        public override void Handle(ListingCreated @event)
        {
            _listings.Save(@event.Listing);
        }
    }
}