namespace DDD.Light.Realtor.Domain.Model.Buyer
{
    // value object
    public class Address
    {
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Address(string address1, string address2, string city, string state, string zip)
        {
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
        }
    }
}