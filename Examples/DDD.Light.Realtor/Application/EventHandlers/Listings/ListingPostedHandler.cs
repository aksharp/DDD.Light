using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listings
{
    public class ListingPostedHandler : IEventHandler<ListingPosted>
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Listing> _listingRepo;

        public ListingPostedHandler(IRepository<Domain.Model.Realtor> realtorRepo, IRepository<Listing> listingRepo)
        {
            _realtorRepo = realtorRepo;
            _listingRepo = listingRepo;
        }

        public void Handle(ListingPosted @event)
        {
            _realtorRepo.Save(@event.Realtor);
            _listingRepo.Save(@event.Listing);
        }
    }
}