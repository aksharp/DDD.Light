using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class OfferRejected
    {
        public Offer Offer { get; set; }
    }
}