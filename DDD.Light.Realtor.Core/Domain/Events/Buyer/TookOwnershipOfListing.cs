using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot;

namespace DDD.Light.Realtor.Core.Domain.Events.Buyer
{
    public class TookOwnershipOfListing
    {
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}