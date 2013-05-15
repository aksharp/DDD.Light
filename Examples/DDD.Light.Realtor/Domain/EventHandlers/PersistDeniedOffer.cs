using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class PersistDeniedOffer : IEventHandler<OfferDenied>
    {
        private readonly IRepository<Offer> _offerRepository;

        public PersistDeniedOffer(IRepository<Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Handle(OfferDenied offerDenied)
        {
            _offerRepository.Save(offerDenied.Offer);
        }
    }
}