using System.Collections.Generic;

namespace DDD.Light.Realtor.Models
{
    public class ReturningBuyer : Buyer
    {
        public IEnumerable<Property> Properties { get; set; }
    }
}