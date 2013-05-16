using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offers
{
    public class OfferRejectedHandler : IEventHandler<OfferRejected>
    {
         private readonly IRepository<Offer> _offerRepo;
        private readonly IRepository<IBuyer> _buyerRepo;

        public OfferRejectedHandler(IRepository<Offer> offerRepo, IRepository<IBuyer> buyerRepo)
         {
             _offerRepo = offerRepo;
             _buyerRepo = buyerRepo;
         }

        public void Handle(OfferRejected @event)
        {
            _offerRepo.Save(@event.Offer);
            var buyer = _buyerRepo.GetById(@event.Offer.BuyerId);
            buyer.NotifyOfRejectedOffer(@event.Offer);
        }
    }
}