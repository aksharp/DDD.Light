using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Offer;

namespace DDD.Light.Realtor.Core.Domain.Model.Offer.AggregateRoot
{
    // aggregate root
    public class Offer : Entity
    {
        private readonly Guid _buyerId;
        private readonly Guid _listingId;
        private readonly decimal _price;
        private readonly DateTime _offeredOn;
        private IOfferReply _offerReply;

        public Offer(Guid id, Guid buyerId, Guid listingId, decimal price, DateTime offeredOn, IOfferReply offerReply) : base(id)
        {
            _buyerId = buyerId;
            _listingId = listingId;
            _price = price;
            _offeredOn = offeredOn;
            _offerReply = offerReply;
        }

        public void Accept()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            _offerReply = new OfferAcceptance(DateTime.UtcNow);
            EventBus.Instance.Publish(new Accepted{ Offer = this });
        }
        
        public void Reject()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            _offerReply = new OfferDenial(DateTime.UtcNow);
            EventBus.Instance.Publish(new Rejected{ Offer = this });
        }
    }
}