using System;
using System.Collections.Generic;
using DDD.Light.Realtor.Core.Domain.Model;
using DDD.Light.Realtor.Core.Domain.Model.Listing;
using DDD.Light.Realtor.Core.Domain.Model.Listing.AggregateRoot;

namespace DDD.Light.Realtor.API.Queries
{
    public interface IListings
    {
        IEnumerable<Listing> All();
        Listing ById(Guid id);
        IEnumerable<Listing> UnderMillionDollars();
    }
}