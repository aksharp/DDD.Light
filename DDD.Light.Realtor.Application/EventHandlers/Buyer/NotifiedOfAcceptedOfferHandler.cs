using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyer
{
    public class NotifiedOfAcceptedOfferHandler : EventHandler<Core.Domain.Events.Buyer.NotifiedOfAcceptedOffer>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public NotifiedOfAcceptedOfferHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(Core.Domain.Events.Buyer.NotifiedOfAcceptedOffer @event)
        {
            _buyerRepo.Save(@event.Buyer);
        }
    }
}