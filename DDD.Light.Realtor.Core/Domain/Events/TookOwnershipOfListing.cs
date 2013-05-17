using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class TookOwnershipOfListing
    {
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}