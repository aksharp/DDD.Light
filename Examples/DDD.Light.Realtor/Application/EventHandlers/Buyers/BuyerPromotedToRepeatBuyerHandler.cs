using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyers
{
    public class BuyerPromotedToRepeatBuyerHandler : IEventHandler<BuyerPromotedToRepeatBuyer>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public BuyerPromotedToRepeatBuyerHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public void Handle(BuyerPromotedToRepeatBuyer @event)
        {
            _buyerRepo.Save(@event.Buyer);
        }
    }
}