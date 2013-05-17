using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyer
{
    public class TookOwnershipOfListingHandler : EventHandler<Core.Domain.Events.Buyer.TookOwnershipOfListing>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public TookOwnershipOfListingHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(Core.Domain.Events.Buyer.TookOwnershipOfListing @event)
        {
            _buyerRepo.Save(@event.RepeatBuyer);
        }
    }
}