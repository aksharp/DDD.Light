using DDD.Light.Realtor.Core.Domain.Model.Buyer;

namespace DDD.Light.Realtor.Core.Domain.Events.Buyer
{
    public class NotifiedOfAcceptedOffer
    {
        public IBuyer Buyer { get; set; }
    }
}