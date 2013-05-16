using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class TookOwnershipOfListing
    {
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}