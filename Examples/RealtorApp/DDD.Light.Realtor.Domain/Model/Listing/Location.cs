namespace DDD.Light.Realtor.Domain.Model.Listing
{
    // value object
    public class Location
    {
        public string Street { get; private  set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Location(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }

    }
}