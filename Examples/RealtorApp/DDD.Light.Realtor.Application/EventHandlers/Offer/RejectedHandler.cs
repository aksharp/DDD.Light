using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offer
{
    public class RejectedHandler : EventHandler<Core.Domain.Events.Offer.Rejected>
    {
         private readonly IRepository<Core.Domain.Model.Offer.AggregateRoot.Offer> _offerRepo;
        private readonly IRepository<IBuyer> _buyerRepo;

        public RejectedHandler(IRepository<Core.Domain.Model.Offer.AggregateRoot.Offer> offerRepo, IRepository<IBuyer> buyerRepo)
         {
             _offerRepo = offerRepo;
             _buyerRepo = buyerRepo;
         }

        public override void Handle(Core.Domain.Events.Offer.Rejected @event)
        {
            _offerRepo.Save(@event.Offer);
            var buyer = _buyerRepo.GetById(@event.Offer.BuyerId);
            buyer.NotifyOfRejectedOffer(@event.Offer);
        }
    }
}