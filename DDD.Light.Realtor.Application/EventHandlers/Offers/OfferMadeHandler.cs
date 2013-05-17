using System;
using DDD.Light.Realtor.Core.Domain.Events;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offers
{
    public class OfferMadeHandler : Messaging.EventHandler<OfferMade>
    {
        private readonly IRepository<Core.Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Offer> _offerRepo;

        public OfferMadeHandler(IRepository<Core.Domain.Model.Realtor> realtorRepo, IRepository<Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public override void Handle(OfferMade @event)
        {
            _offerRepo.Save(@event.Offer);
            var realtor = _realtorRepo.GetById(Guid.Empty);
            realtor.NotifyThatOfferWasMade(@event.Offer.Id);
            _realtorRepo.Save(realtor);
        }
    }
}