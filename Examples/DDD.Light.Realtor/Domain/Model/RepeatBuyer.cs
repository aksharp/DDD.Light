using System.Collections.Generic;
using System.Linq;

namespace DDD.Light.Realtor.Domain.Model
{
    public class RepeatBuyer : Buyer
    {
        public RepeatBuyer()
        {
            Properties = new List<Property>();
        }

        public void PurchaseProperty(Listing listing)
        {
            Properties.ToList().Add(new Property(listing));
        }
        public IEnumerable<Property> Properties { get; set; }
    }
}