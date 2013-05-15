using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Realtor.Domain.Services;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class ConvertListingToBuyersProperty : IEventHandler<OfferAccepted>
    {
        private readonly IRepository<IBuyer> _buyerRepo;
        private readonly IBuyerService _buyerService;
        private readonly IRepository<Listing> _listingRepo;

        public ConvertListingToBuyersProperty(IRepository<IBuyer> buyerRepo, IBuyerService buyerService, IRepository<Listing> listingRepo)
        {
            _buyerRepo = buyerRepo;
            _buyerService = buyerService;
            _listingRepo = listingRepo;
        }

        public void Handle(OfferAccepted offerAccepted)
        {
            var repeatBuyer = GetRepeatBuyer(offerAccepted);
            var listing = _listingRepo.GetById(offerAccepted.Offer.ListingId);
            repeatBuyer.PurchaseProperty(listing);
            _buyerRepo.Save(repeatBuyer);
        }

        private RepeatBuyer GetRepeatBuyer(OfferAccepted offerAccepted)
        {
            var buyer = _buyerRepo.GetById(offerAccepted.Offer.BuyerId);
            if (!(buyer is RepeatBuyer))
                buyer = _buyerService.PromoteBuyerToRepeatBuyer(offerAccepted.Offer.BuyerId);
            var repeatBuyer = buyer as RepeatBuyer;
            if (repeatBuyer == null) throw new Exception("Buyer was not found");
            return repeatBuyer;
        }
    }
}