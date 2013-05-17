using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offers
{
    public class OfferAcceptedHandler : EventHandler<OfferAccepted>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public OfferAcceptedHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(OfferAccepted @event)
        {
            var buyer = _buyerRepo.GetById(@event.Offer.BuyerId);
            buyer.NotifyOfRejectedOffer(@event.Offer);

        }
    }
}