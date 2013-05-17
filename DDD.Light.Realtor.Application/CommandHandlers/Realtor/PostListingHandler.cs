using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.API.Commands.Realtor;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.CommandHandlers.Realtor
{
    public class PostListingHandler : CommandHandler<PostListingCommand>
    {
        private readonly IRepository<Core.Domain.Model.Realtor> _realtorRepo;

        public PostListingHandler(IRepository<Core.Domain.Model.Realtor> realtorRepo)
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