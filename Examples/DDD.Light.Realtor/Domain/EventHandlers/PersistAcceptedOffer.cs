using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class PersistAcceptedOffer : IEventHandler<OfferAccepted>
    {
        private readonly IRepository<Offer> _offerRepository;

        public PersistAcceptedOffer(IRepository<Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Handle(OfferAccepted offerMade)
        {
            _offerRepository.Save(offerMade.Offer);
        }
    }
}