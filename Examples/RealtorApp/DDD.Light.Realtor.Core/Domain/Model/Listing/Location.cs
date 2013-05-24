namespace DDD.Light.Realtor.Core.Domain.Model.Listing
{
    // value object
    public class Location
    {
        public Location(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
    }
}