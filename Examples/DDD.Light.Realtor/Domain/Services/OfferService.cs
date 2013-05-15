using System;
using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Services
{
    public class OfferService : IEventHandler<OfferMade>
    {
        private readonly IRepository<Offer> _offerRepository;
        private readonly IRepository<Model.Realtor> _realtorRepository;

        public OfferService(IRepository<Offer> offerRepository, IRepository<Model.Realtor> realtorRepository)
        {
            _offerRepository = offerRepository;
            _realtorRepository = realtorRepository;
        }

        public void Handle(OfferMade offerMade)
        {
            SaveOffer(offerMade);
            NotifyRealtoOfAnOffer(offerMade);
        }

        private void NotifyRealtoOfAnOffer(OfferMade offerMade)
        {
            var realtor = _realtorRepository.GetById(Guid.Empty);
            realtor.NotifyThatOfferWasMade(offerMade.OfferId);
            _realtorRepository.Save(realtor);
        }

        private void SaveOffer(OfferMade offerMade)
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