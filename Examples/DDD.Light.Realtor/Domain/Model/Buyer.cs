using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    public class Buyer : Entity, IBuyer
    {
        public Buyer()
        {
            OfferIds = new List<Guid>();
        }

        public IEnumerable<Guid> OfferIds { get; set; }
        public Prospect Prospect { get; set; }

        public virtual void MakeAnOffer(Guid listingId, decimal price)
        {
            if (Id == null) throw new Exception("Buyer does not have an Id");
            var offerId = Guid.NewGuid();
            OfferIds.ToList().Add(offerId);
            var offerMade = new OfferMade{BuyerId = Id.Value, ListingId =  listingId, OfferId = offerId, Price = price};
            EventBus.Instance.Publish(offerMade);
        }

    }
}