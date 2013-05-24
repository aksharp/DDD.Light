using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Offer
{
    // value object
    public class OfferAcceptance : IOfferReply
    {
        public OfferAcceptance(DateTime repliedOn)
        {
            RepliedOn = repliedOn;
        }

        public DateTime RepliedOn { get; private set; }
    }
}