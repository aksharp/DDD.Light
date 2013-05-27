using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Realtor.API.Query.Contract;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.API.Query
{
    public class ActiveListings : IActiveListings
    {
        private readonly IRepository<Listing> _listingsRepo;

        public ActiveListings(IRepository<Listing> listingsRepo)
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
