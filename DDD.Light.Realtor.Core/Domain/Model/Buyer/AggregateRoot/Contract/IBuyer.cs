using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract
{
    public interface IBuyer : IEntity
    {
        void NotifyOfRejectedOffer(Offer.AggregateRoot.Offer offer);
        void PromoteToRepeatBuyer();
        void PurchaseProperty(Listing.AggregateRoot.Listing listing);
        List<Guid> OfferIds { get; set; }
        Prospect.AggregateRoot.Prospect Prospect { get; set; }
    }
}