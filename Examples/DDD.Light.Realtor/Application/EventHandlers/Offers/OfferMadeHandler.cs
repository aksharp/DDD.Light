using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offers
{
    public class OfferMadeHandler : IEventHandler<OfferMade>
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Offer> _offerRepo;

        public OfferMadeHandler(IRepository<Domain.Model.Realtor> realtorRepo, IRepository<Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public void Handle(OfferMade @event)
        {
            _offerRepo.Save(@event.Offer);
            var realtor = _realtorRepo.GetById(Guid.Empty);
            realtor.NotifyThatOfferWasMade(@event.Offer.Id);
            _realtorRepo.Save(realtor);
        }
    }
}