using System.Collections.Generic;

namespace DDD.Light.Realtor.Domain.Model
{
    public class ReturningBuyer : Buyer
    {
        public IEnumerable<Property> Properties { get; set; }
    }
}