using System;
using DDD.Light.Realtor.Domain.Model.Listing;

namespace DDD.Light.Realtor.Domain.Event.Listing
{
    public class ListingCreated
    {
        public Guid Id { get; private set; }
        public Location Location { get; private set; }
        public Description Description { get; private set; }
        public decimal Price { get; private set; }

        public ListingCreated(Guid id, Location location, Description description, decimal price)
        {
            Id = id;
            Location = location;
            Description = description;
            Price = price;
        }
    }
}