using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class OfferRejected
    {
        public Offer Offer { get; set; }
    }
}