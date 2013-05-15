using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class DeactivateListing : IEventHandler<OfferAccepted>
    {
        private readonly IRepository<Listing> _listingRepo;

        public DeactivateListing(IRepository<Listing> listingRepo)
        {
            _listingRepo = listingRepo;
        }

        public void Handle(OfferAccepted offerAccepted)
        {
            var listing = _listingRepo.GetById(offerAccepted.Offer.ListingId);
            listing.Deactivate();
            _listingRepo.Save(listing);
        }

    }
}