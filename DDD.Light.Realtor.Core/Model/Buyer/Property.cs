using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer
{
    // value object
    public class Property
    {
        public Address Address { get; private set; }
        public Guid ListingId { get; private set; }

        public Property(Guid listingId, Address address)
        {
            ListingId = listingId;
            Address = address;
        }
    }
}