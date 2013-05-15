using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class OfferAccepted
    {
        public Offer Offer { get; set; }
    }
}