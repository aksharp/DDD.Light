namespace DDD.Light.Realtor.ReadModel.DAO
{
    public class ActiveListing : Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int YearBuilt { get; set; }
        public decimal Price { get; set; }
    }
}
