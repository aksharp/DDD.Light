using System;
using System.Collections.Generic;
using DDD.Light.Realtor.API.Query.Model;

namespace DDD.Light.Realtor.API.Query.Contract
{
    public interface IRealtorListings 
    {
        IEnumerable<Listing> All();
        Listing ById(Guid id);
        IEnumerable<Listing> Posted();
        IEnumerable<Listing> New();
    }
}