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

        public List<Guid> OfferIds { get; set; }
        public Prospect Prospect { get; set; }

        public void NotifyOfAcceptedOffer(Offer offer)
        {
            OfferIds.Add(offer.Id);
            EventBus.Instance.Publish(new RepeatBuyerNotifiedOfAcceptedOffer { Buyer = this });
        }

        public void NotifyOfRejectedOffer(Offer offer)
        {
            throw new NotImplementedException();
        }

        public void PurchaseProperty(Listing listing)
        {
            PromoteToRepeatBuyer();
        }

        public virtual void MakeAnOffer(Guid listingId, decimal price)
        {
            var offerId = Guid.NewGuid();
            var offer = new Offer {Id = offerId, BuyerId = Id, ListingId = listingId, Price = price};
            OfferIds.Add(offerId);
            var offerMade = new OfferMade{Offer = offer};
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
            EventBus.Instance.Publish(new BuyerPromotedToRepeatBuyer { Buyer = this, RepeatBuyer = repeatBuyer });
        }

    }
}