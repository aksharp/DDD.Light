using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    public interface IBuyer : IEntity
    {
        IEnumerable<Guid> OfferIds { get; set; }
        Prospect Prospect { get; set; }
    }
}