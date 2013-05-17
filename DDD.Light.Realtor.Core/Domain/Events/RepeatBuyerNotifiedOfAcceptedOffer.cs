using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class RepeatBuyerNotifiedOfAcceptedOffer
    {
        public IBuyer Buyer { get; set; }
    }
}