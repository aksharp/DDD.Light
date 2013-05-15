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

        public void Handle(OfferAccepted @event)
        {
            var buyerIsNotARepeatBuyer = _buyerRepo.Get().OfType<Buyer>().Any(x => x.Id == @event.Offer.BuyerId);
            if (buyerIsNotARepeatBuyer)
                PromoteBuyerToARepeatBuyer(@event.Offer.BuyerId, @event.Offer.ListingId);
        }

        private void PromoteBuyerToARepeatBuyer(Guid buyerId, Guid listingId)
        {
            var buyer = _buyerRepo.GetById(buyerId);
            var repeatBuyer = new RepeatBuyer
                {
                    Id = buyer.Id,
                    OfferIds = buyer.OfferIds,
                    Prospect = buyer.Prospect
                };
            repeatBuyer.Properties.ToList().Add(new Property{ListingId = listingId});
            _buyerRepo.Delete(buyer);
            _buyerRepo.Save(repeatBuyer);
        }
    }
}