using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class OfferMade
    {
        public Offer Offer { get; set; }
    }
}