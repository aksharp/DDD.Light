namespace DDD.Light.Realtor.Core.Domain.Events.Offer
{
    public class Accepted
    {
        public Model.Offer.AggregateRoot.Offer Offer { get; set; }
    }
}