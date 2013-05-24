namespace DDD.Light.Realtor.Core.Domain.Events.Offer
{
    public class Rejected
    {
        public Model.Offer.AggregateRoot.Offer Offer { get; set; }
    }
}