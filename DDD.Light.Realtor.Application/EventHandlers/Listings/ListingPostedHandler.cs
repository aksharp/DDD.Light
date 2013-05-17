using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listings
{
    public class ListingPostedHandler : EventHandler<ListingPosted>
    {
        private readonly IRepository<Core.Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Listing> _listingRepo;

        public ListingPostedHandler(IRepository<Core.Domain.Model.Realtor> realtorRepo, IRepository<Listing> listingRepo)
        {
            _realtorRepo = realtorRepo;
            _listingRepo = listingRepo;
        }

        public override void Handle(ListingPosted @event)
        {
            _realtorRepo.Save(@event.Realtor);
            _listingRepo.Save(@event.Listing);
        }
    }
}