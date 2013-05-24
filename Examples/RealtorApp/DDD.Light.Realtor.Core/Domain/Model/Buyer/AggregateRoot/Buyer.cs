using System;
using System.Collections.Generic;
using DDD.Light.Realtor.Core.Domain.Events.Buyer;
using DDD.Light.Realtor.Core.Domain.Events.Offer;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot
{
    public class Buyer : Entity, IBuyer
    {
        public Buyer()
        {
            OfferIds = new List<Guid>();
        }

        public List<Guid> OfferIds { get; set; }
        public Prospect.AggregateRoot.Prospect Prospect { get; set; }

        public void NotifyOfAcceptedOffer(Offer.AggregateRoot.Offer offer)
        {
            OfferIds.Add(offer.Id);
            EventBus.Instance.Publish(new NotifiedOfAcceptedOffer { Buyer = this });
        }

        public void NotifyOfRejectedOffer(Offer.AggregateRoot.Offer offer)
        {
            throw new NotImplementedException();
        }

        public void PurchaseProperty(Listing.AggregateRoot.Listing listing)
        {
            PromoteToRepeatBuyer();
        }

        public virtual void MakeAnOffer(Guid listingId, decimal price)
        {
            var offerId = Guid.NewGuid();
            var offer = new Offer.AggregateRoot.Offer {Id = offerId, BuyerId = Id, ListingId = listingId, Price = price};
            OfferIds.Add(offerId);
            var offerMade = new Made{Offer = offer};
            EventBus.Instance.Publish(offerMade);
        }

        public void PromoteToRepeatBuyer()
        {
            var repeatBuyer = new RepeatBuyer
            {
                Id = Id,
                OfferIds = OfferIds,
                Prospect = Prospect
            };
            EventBus.Instance.Publish(new PromotedToRepeatBuyer { Buyer = this, RepeatBuyer = repeatBuyer });
        }

    }
}