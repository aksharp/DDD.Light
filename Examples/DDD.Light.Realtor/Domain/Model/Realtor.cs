using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Realtor : Entity
    {
        public Realtor()
        {
            Offers = new List<Guid>();
        }

        public IEnumerable<Guid> Offers { get; set; }

        public void NotifyThatOfferWasMade(Guid offerId)
        {
            Offers.ToList().Add(offerId);
        }

    }
}