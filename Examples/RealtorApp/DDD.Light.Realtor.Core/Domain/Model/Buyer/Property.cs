using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer
{
    // value object
    public class Property
    {
        public Property()
        {
            Address = new Address();
        }

        public Property(Listing.AggregateRoot.Listing listing)
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