using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Application.Commands;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.CommandHandlers.Realtor
{
    public class PostListingHandler : CommandHandler<PostListingCommand>
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;

        public PostListingHandler(IRepository<Domain.Model.Realtor> realtorRepo)
        {
            _realtorRepo = realtorRepo;
        }

        public override void Handle(PostListingCommand command)
        {
            var listing = new Listing
                {
                    Id = command.ListingId,
                    Active = true,
                    Description = new Description
                        {
                            NumberOfBathrooms = command.NumberOfBathrooms,
                            NumberOfBedrooms = command.NumberOfBedrooms,
                            YearBuilt = command.YearBuilt
                        },
                        Location = new Location
                            {
                                City = command.City,
                                Street = command.Street,
                                State = command.State,
                                Zip = command.Zip
                            }
                };
            var realtor = _realtorRepo.Get().First();
            realtor.PostListing(listing);
        }
    }
}