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

        public Address Address { get; set; }
        public Guid ListingId { get; set; }
    }
}