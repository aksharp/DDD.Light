using System.Linq;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.Realtor.Domain.Model.Listing;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.CommandHandler.Realtor
{
    public class PostListingHandler : CommandHandler<PostListing>
    {
        private readonly IRepository<Domain.Model.Realtor.Realtor> _realtorRepo;

        public PostListingHandler(IRepository<Domain.Model.Realtor.Realtor> realtorRepo)
        {
            _realtorRepo = realtorRepo;
        }

        public override void Handle(PostListing command)
        {
            var listing = new Listing(
                command.ListingId,
                new Location(command.Street, command.City, command.State, command.Zip),
                new Description(command.NumberOfBathrooms, command.NumberOfBedrooms, command.YearBuilt),                
                command.Price
            );

            var realtor = _realtorRepo.Get().First();
            realtor.PostListing(listing.Id);
        }
    }
}