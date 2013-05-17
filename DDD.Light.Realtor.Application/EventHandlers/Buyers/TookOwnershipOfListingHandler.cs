using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyers
{
    public class TookOwnershipOfListingHandler : EventHandler<TookOwnershipOfListing>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public TookOwnershipOfListingHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(TookOwnershipOfListing @event)
        {
            _buyerRepo.Save(@event.RepeatBuyer);
        }
    }
}