using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offer
{
    public class MadeHandler : Messaging.InProcess.EventHandler<Core.Domain.Events.Offer.Made>
    {
        private readonly IRepository<Core.Domain.Model.Realtor.AggregateRoot.Realtor> _realtorRepo;
        private readonly IRepository<Core.Domain.Model.Offer.AggregateRoot.Offer> _offerRepo;

        public MadeHandler(IRepository<Core.Domain.Model.Realtor.AggregateRoot.Realtor> realtorRepo, IRepository<Core.Domain.Model.Offer.AggregateRoot.Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public override void Handle(Core.Domain.Events.Offer.Made @event)
        {
            _offerRepo.Save(@event.Offer);
            var realtor = _realtorRepo.GetById(Guid.Empty);
            realtor.NotifyThatOfferWasMade(@event.Offer.Id);
            _realtorRepo.Save(realtor);
        }
    }
}