using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listings
{
    public class ListingDeactivatedHandler : EventHandler<ListingDeactivated>
    {
        private readonly IRepository<Listing> _listingRepo;

        public ListingDeactivatedHandler(IRepository<Listing> listingRepo)
        {
            _listingRepo = listingRepo;
        }

        public override void Handle(ListingDeactivated @event)
        {
            _listingRepo.Save(@event.Listing);
        }
    }
}