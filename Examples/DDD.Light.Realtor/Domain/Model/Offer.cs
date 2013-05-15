using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Offer : Entity
    {
        public Offer()
        {
            
        }

        public Offer(BuyerMadeAnOffer buyerMadeAnOffer)
        {
            BuyerId = buyerMadeAnOffer.BuyerId;
            Id = buyerMadeAnOffer.OfferId;
            OfferedOn = DateTime.UtcNow;
            ListingId = buyerMadeAnOffer.ListingId;
            Price = buyerMadeAnOffer.Price;
        }

        public Guid BuyerId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferedOn { get; set; }
        public IOfferReply OfferReply { get; set; }

        public void Accept()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferAcceptance{ RepliedOn = DateTime.UtcNow };
            var offerAccepted = new OfferAccepted{ Offer = this };
            EventBus.Instance.Publish(offerAccepted);
        }
        
        public void Deny()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferDenial{ RepliedOn = DateTime.UtcNow };
            var offerAccepted = new OfferDenied{ Offer = this };
            EventBus.Instance.Publish(offerAccepted);
        }
    }
}