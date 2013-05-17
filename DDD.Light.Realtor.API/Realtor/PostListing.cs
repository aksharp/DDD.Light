using System;

namespace DDD.Light.Realtor.API.Commands.Realtor
{
    public class PostListing
    {
        public Guid RealtorId { get; set; }
        public Guid ListingId { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int YearBuilt { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }        
        public string Zip { get; set; }
    }
}