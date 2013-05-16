using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class RepeatBuyerNotifiedOfAcceptedOffer
    {
        public IBuyer Buyer { get; set; }
    }
}