using System;

namespace DDD.Light.Realtor.Domain.Model
{
    // value object
    public class Property
    {
        public Property()
        {
            Address = new Address();
        }

        public Property(Listing listing)
        {
            ListingId = listing.Id;
            Address = new Address
                {
                    Address1 = listing.Location.Street,
                    City = listing.Location.City,
                    State = listing.Location.State,
                    Zip = listing.Location.Zip
                };
        }

        public Address Address { get; set; }
        public Guid ListingId { get; set; }
    }
}