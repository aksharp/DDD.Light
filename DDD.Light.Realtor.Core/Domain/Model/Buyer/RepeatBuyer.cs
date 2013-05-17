using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Buyer;

namespace DDD.Light.Realtor.Core.Domain.Model.Buyer
{
    public class RepeatBuyer : Buyer
    {
        public RepeatBuyer()
        {
            Properties = new List<Property>();
        }

        public List<Property> Properties { get; set; }
        

       

        public void TakeOwnershipOf(Listing.Listing listing)
        {
            Properties.Add(new Property(listing));
            EventBus.Instance.Publish(new TookOwnershipOfListing{RepeatBuyer = this});
        }
        
    }
}