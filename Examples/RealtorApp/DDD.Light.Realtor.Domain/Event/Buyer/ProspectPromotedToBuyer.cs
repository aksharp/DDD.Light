using System;

namespace DDD.Light.Realtor.Domain.Event.Buyer
{
    public class ProspectPromotedToBuyer
    {
        public Guid BuyerId { get; private set; }
        public Guid ProspectId { get; private set; }

        public ProspectPromotedToBuyer(Guid buyerId, Guid prospectId)
        {
            BuyerId = buyerId;
            ProspectId = prospectId;
        }
    }
}