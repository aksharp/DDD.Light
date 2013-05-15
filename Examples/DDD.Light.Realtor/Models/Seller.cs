using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    // aggregate root
    public class Seller : Entity
    {
        public Seller()
        {
            Offers = new List<Offer>();
        }

        public string Email { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}