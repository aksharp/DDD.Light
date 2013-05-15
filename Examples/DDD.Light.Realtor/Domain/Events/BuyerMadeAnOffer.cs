using System;

namespace DDD.Light.Realtor.Domain.Events
{
    public class BuyerMadeAnOffer
    {
        public Guid OfferId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
    }
}