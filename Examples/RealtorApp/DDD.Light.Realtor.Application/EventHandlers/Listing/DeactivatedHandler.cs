using DDD.Light.Messaging;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listing
{
    public class DeactivatedHandler : EventHandler<Core.Domain.Events.Listing.Deactivated>
    {
        private readonly IRepository<Core.Domain.Model.Listing.AggregateRoot.Listing> _listingRepo;

        public DeactivatedHandler(IRepository<Core.Domain.Model.Listing.AggregateRoot.Listing> listingRepo)
        {
            _listingRepo = listingRepo;
        }

        public override void Handle(Core.Domain.Events.Listing.Deactivated @event)
        {
            _listingRepo.Save(@event.Listing);
        }
    }
}