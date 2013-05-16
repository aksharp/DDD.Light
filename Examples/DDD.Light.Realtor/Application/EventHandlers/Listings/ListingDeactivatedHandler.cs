using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listings
{
    public class ListingDeactivatedHandler : IEventHandler<ListingDeactivated>
    {
         private readonly IRepository<Listing> _listingRepo;


        public ListingDeactivatedHandler(IRepository<Listing> listingRepo)
        {
            _listingRepo = listingRepo;
        }

        public void Handle(ListingDeactivated @event)
        {
            _listingRepo.Save(@event.Listing);
        }
    }
}