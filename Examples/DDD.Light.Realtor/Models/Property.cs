namespace DDD.Light.Realtor.Models
{
    // value object
    public class Property
    {
        public Property()
        {
            Address = new Address();
            Listing = new Listing();
        }

        public Address Address { get; set; }
        public Listing Listing { get; set; }
    }
}