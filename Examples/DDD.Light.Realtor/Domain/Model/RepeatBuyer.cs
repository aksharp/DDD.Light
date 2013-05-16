using System.Collections.Generic;
using System.Linq;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;

namespace DDD.Light.Realtor.Domain.Model
{
    public class RepeatBuyer : Buyer
    {
        public RepeatBuyer()
        {
            Properties = new List<Property>();
        }

        public List<Property> Properties { get; set; }
        

       

        public void TakeOwnershipOf(Listing listing)
        {
            Properties.Add(new Property(listing));
            EventBus.Instance.Publish(new TookOwnershipOfListing{RepeatBuyer = this});
        }
        
    }
}