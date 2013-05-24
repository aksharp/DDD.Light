using System;

namespace DDD.Light.Realtor.Domain.Model.Offer
{
    // value object
    public class OfferDenial : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}