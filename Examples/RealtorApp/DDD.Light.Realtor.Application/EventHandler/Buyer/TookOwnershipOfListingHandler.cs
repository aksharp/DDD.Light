using DDD.Light.Realtor.Domain.Event.Buyer;
using DDD.Light.Repo.Contracts;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Buyer
{
    public class TookOwnershipOfListingHandler : EventHandler<TookOwnershipOfListing>
    {
        private readonly IRepository<Domain.Model.Buyer.Buyer> _buyerRepo;

        public TookOwnershipOfListingHandler(IRepository<Domain.Model.Buyer.Buyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(TookOwnershipOfListing @event)
        {
        }
    }
}