using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Buyer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Buyer
{
    public class TookOwnershipOfListingHandler : EventHandler<TookOwnershipOfListing>
    {
        private readonly IRepository<Core.Domain.Model.Buyer.Buyer> _buyerRepo;

        public TookOwnershipOfListingHandler(IRepository<Core.Domain.Model.Buyer.Buyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(TookOwnershipOfListing @event)
        {
        }
    }
}