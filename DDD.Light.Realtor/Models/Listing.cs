using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class PropertyInfo
    {
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int YearBuilt { get; set; }
    }

    public class Listing : Entity
    {
        public Listing()
        {
            Address = new Address();
            PropertyInfo = new PropertyInfo();
        }

        public Address Address { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }

}