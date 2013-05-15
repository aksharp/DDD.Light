using System;

namespace DDD.Light.Realtor.Domain.Events
{
    public class OfferMade
    {
        public Guid OfferId { get; set; }
        public Guid ProspectId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
    }
}