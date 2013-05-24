using System;
using System.Collections.Generic;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.API.Query.Contract
{
    public interface IActiveListings 
    {
        IEnumerable<ActiveListing> All();
        ActiveListing ById(Guid id);
        IEnumerable<ActiveListing> UnderMillionDollars();
    }
}