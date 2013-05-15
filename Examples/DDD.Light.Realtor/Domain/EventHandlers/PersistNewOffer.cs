using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class PersistNewOffer : IEventHandler<BuyerMadeAnOffer>
    {
        private readonly IRepository<Offer> _offerRepository;

        public PersistNewOffer(IRepository<Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Handle(BuyerMadeAnOffer buyerMadeAnOffer)
        {
            var offer = new Offer(buyerMadeAnOffer);
            _offerRepository.Save(offer);
        }
    }
}