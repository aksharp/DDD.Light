using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class OfferDenied
    {
        public Offer Offer { get; set; }
    }
}