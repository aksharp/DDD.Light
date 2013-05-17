using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Offer
{
    // value object
    public class OfferAcceptance : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}