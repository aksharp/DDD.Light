using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Offer : Entity
    {
        public Guid BuyerId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferedOn { get; set; }
        public IOfferReply OfferReply { get; set; }
    }
}