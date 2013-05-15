using System;

namespace DDD.Light.Realtor.Models
{
    // value object
    public class Offer
    {
        public IBuyer Buyer { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferedOn { get; set; }
        public IOfferReply OfferReply { get; set; }
    }
}