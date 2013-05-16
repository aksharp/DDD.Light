using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    public interface IBuyer : IEntity
    {
        void NotifyOfRejectedOffer(Offer offer);
        void PromoteToRepeatBuyer();
        void PurchaseProperty(Listing listing);
        IEnumerable<Guid> OfferIds { get; set; }
        Prospect Prospect { get; set; }
    }
}