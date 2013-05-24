using System;
using System.Collections.Generic;
using DDD.Light.Realtor.ReadModel.DAO;

namespace DDD.Light.Realtor.ReadModel
{
    public interface IActiveListings 
    {
        IEnumerable<ActiveListing> All();
        ActiveListing ById(Guid id);
        IEnumerable<ActiveListing> UnderMillionDollars();
    }
}