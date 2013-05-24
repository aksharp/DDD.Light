using DDD.Light.EventStore.Contracts;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Realtor.Domain.Event.Listing;
using DDD.Light.Realtor.Domain.Event.Realtor;
using DDD.Light.Repo.Contracts;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Listing
{
    public class ListingPostedHandler : EventHandler<ListingPosted>
    {
        private readonly IRepository<ActiveListing> _activeListings;

        public ListingPostedHandler(IRepository<ActiveListing> activeListings)
        {
            _activeListings = activeListings;
        }

        public override void Handle(ListingPosted @event)
        {
            var activeListing = new ActiveListing
                {
                    Id = @event.Id,
                    NumberOfBathrooms = @event.NumberOfBathrooms,
                    NumberOfBedrooms = @event.NumberOfBedrooms,
                    YearBuilt = @event.YearBuilt,
                    Street = @event.Street,
                    City = @event.City,
                    State = @event.State,
                    Zip = @event.Zip,
                    Price = @event.Price
                };
            _activeListings.Save(activeListing);
        }
    }
}