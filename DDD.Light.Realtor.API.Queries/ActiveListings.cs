using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Realtor.ReadModel.DAO;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.ReadModel
{
    public class ActiveListings : IActiveListings
    {
        private readonly IRepository<ActiveListing> _listingsRepo;

        public ActiveListings(IRepository<ActiveListing> listingsRepo)
        {
            _listingsRepo = listingsRepo;
        }

        public IEnumerable<ActiveListing> All()
        {
            return _listingsRepo.GetAll();
        }

        public ActiveListing ById(Guid id)
        {
            return _listingsRepo.GetById(id);
        }

        public IEnumerable<ActiveListing> UnderMillionDollars()
        {
            return _listingsRepo.Get().Where(l => l.Price < 1000000);
        }

    }
}
