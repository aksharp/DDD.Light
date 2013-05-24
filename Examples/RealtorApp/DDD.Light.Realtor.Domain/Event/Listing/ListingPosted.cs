using System;

namespace DDD.Light.Realtor.Domain.Event.Listing
{
    public class ListingPosted
    {
        public Guid Id { get; private set; }
        public int NumberOfBathrooms { get; private set; }
        public int NumberOfBedrooms { get; private set; }
        public int YearBuilt { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public decimal Price { get; private set; }

        public ListingPosted(
            Guid id, 
            int numberOfBathrooms, 
            int numberOfBedrooms, 
            int yearBuilt, 
            string street, 
            string city, 
            string state, 
            string zip, 
            decimal price)
        {
            Id = id;
            NumberOfBathrooms = numberOfBathrooms;
            NumberOfBedrooms = numberOfBedrooms;
            YearBuilt = yearBuilt;
            Street = street;
            City = city;
            State = state;
            Zip = zip;
            Price = price;
        }
    }
}