using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.API.Queries
{
    public class Listings : IListings
    {
        private readonly IRepository<Listing> _listingsRepo;

        public Listings(IRepository<Listing> listingsRepo)
        {
            _listingsRepo = listingsRepo;
        }

        public IEnumerable<Listing> All()
        {
            return _listingsRepo.GetAll();
        }
        
        public Listing ById(Guid id)
        {
            return _listingsRepo.GetById(id);
        }
        
        public IEnumerable<Listing> UnderMillionDollars()
        {
            return _listingsRepo.Get().Where(l => l.Price < 1000000);
        }

    }
}
