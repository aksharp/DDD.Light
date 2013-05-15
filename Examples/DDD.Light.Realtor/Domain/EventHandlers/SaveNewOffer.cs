using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class SaveNewOffer : IEventHandler<OfferMade>
    {
        private readonly IRepository<Offer> _offerRepository;

        public SaveNewOffer(IRepository<Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Handle(OfferMade offerMade)
        {
            var offer = new Offer
            {
                BuyerId = offerMade.BuyerId,
                Id = offerMade.OfferId,
                OfferedOn = DateTime.UtcNow,
                ListingId = offerMade.ListingId,
                Price = offerMade.Price
            };
            _offerRepository.Save(offer);
        }
    }
}