using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Offer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model.Offer
{
    // aggregate root
    public class Offer : Entity
    {
        public Guid BuyerId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferedOn { get; set; }
        public IOfferReply OfferReply { get; set; }

        public void Accept()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferAcceptance{ RepliedOn = DateTime.UtcNow };
            EventBus.Instance.Publish(new Accepted{ Offer = this });
        }
        
        public void Reject()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferDenial{ RepliedOn = DateTime.UtcNow };
            EventBus.Instance.Publish(new Rejected{ Offer = this });
        }
    }
}