using System;
using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class PromoteBuyerToRepeatBuyer : IEventHandler<OfferAccepted>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public PromoteBuyerToRepeatBuyer(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public void Handle(OfferAccepted buyerMadeAnOffer)
        {
            var buyerIsNotARepeatBuyer = _buyerRepo.Get().OfType<Buyer>().Any(x => x.Id == buyerMadeAnOffer.Offer.BuyerId);
            if (buyerIsNotARepeatBuyer)
                PromoteBuyerToARepeatBuyer(buyerMadeAnOffer.Offer.BuyerId, buyerMadeAnOffer.Offer.ListingId);
        }

        private void PromoteBuyerToARepeatBuyer(Guid buyerId, Guid listingId)
        {
            var buyer = _buyerRepo.GetById(buyerId) as Buyer;
            if (buyer == null) throw new Exception("No buyer with buyerId" + buyerId + " found");
            var repeatBuyer = buyer.PromoteToRepeatBuyer(listingId);
            _buyerRepo.Delete(buyer);
            _buyerRepo.Save(repeatBuyer);
        }
    }
}