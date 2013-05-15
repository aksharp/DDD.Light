using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class PersistNewOffer : IEventHandler<OfferMade>
    {
        private readonly IRepository<Offer> _offerRepository;

        public PersistNewOffer(IRepository<Offer> offerRepository)
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