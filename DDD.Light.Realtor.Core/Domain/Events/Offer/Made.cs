using DDD.Light.Realtor.Core.Domain.Model.Offer;

namespace DDD.Light.Realtor.Core.Domain.Events.Offer
{
    public class Made
    {
        public Model.Offer.AggregateRoot.Offer Offer { get; set; }
    }
}