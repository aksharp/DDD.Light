using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyers
{
    public class RepeatBuyerNotifiedOfAcceptedOfferHandler : EventHandler<RepeatBuyerNotifiedOfAcceptedOffer>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public RepeatBuyerNotifiedOfAcceptedOfferHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(RepeatBuyerNotifiedOfAcceptedOffer @event)
        {
            _buyerRepo.Save(@event.Buyer);
        }
    }
}