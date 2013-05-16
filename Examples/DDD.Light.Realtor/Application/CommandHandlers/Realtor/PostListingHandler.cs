using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.CommandHandlers.Realtor
{
    public class PostListingHandler : ICommandHandler<PostListing>
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;

        public PostListingHandler(IRepository<Domain.Model.Realtor> realtorRepo)
        {
            _realtorRepo = realtorRepo;
        }

        public void Handle(PostListing command)
        {
            var listingId = Guid.NewGuid();
            var listing = new Listing
                {
                    Id = listingId,
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
            var realtor = _realtorRepo.GetById(command.RealtorId);
            realtor.PostListing(listing);
        }
    }
}