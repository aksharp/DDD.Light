namespace DDD.Light.Realtor.Core.Domain.Event.Offer
{
    public class OfferMade
    {
        public Model.Offer.Offer Offer { get; private set; }

        public OfferMade(Model.Offer.Offer offer)
        {
            Offer = offer;
        }
    }
}