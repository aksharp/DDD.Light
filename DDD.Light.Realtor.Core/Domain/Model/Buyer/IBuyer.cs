using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer
{
    public interface IBuyer : IEntity
    {
        void NotifyOfRejectedOffer(Offer.Offer offer);
        void PromoteToRepeatBuyer();
        void PurchaseProperty(Listing.Listing listing);
        List<Guid> OfferIds { get; set; }
        Prospect.Prospect Prospect { get; set; }
    }
}