using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Prospect : Entity
    {
        public IEnumerable<Guid> ListingIdsViewed { get; set; }

        public Buyer PromoteToBuyer(Guid listingId)
        {
            var buyer = new Buyer
            {
                Id = Id,
                Prospect = this
            };
            return buyer;
        }

        public virtual void MakeAnOffer(Guid listingId, decimal price)
        {
            var buyer = PromoteToBuyer(listingId);
            buyer.MakeAnOffer(listingId, price);
        }
    }
}