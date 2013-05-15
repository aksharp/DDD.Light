using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    // aggregate root
    public class Listing : Entity
    {
        public Listing()
        {
            Location = new Location();
            Description = new Description();
        }

        public Location Location { get; set; }
        public Description Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}