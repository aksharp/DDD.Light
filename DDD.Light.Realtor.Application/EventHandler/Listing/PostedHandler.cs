using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Realtor.Core.Domain.Event.Listing;
using DDD.Light.Realtor.Core.Domain.Event.Realtor;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class PostedHandler : EventHandler<PostedListing>
    {
        private readonly IRepository<ActiveListing> _activeListings;

        public PostedHandler(IRepository<ActiveListing> activeListings)
        {
            _activeListings = activeListings;
        }

        public override void Handle(PostedListing @event)
        {
            //todo: implement
        }
    }
}