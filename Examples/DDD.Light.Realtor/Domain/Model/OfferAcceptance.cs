using System;

namespace DDD.Light.Realtor.Domain.Model
{
    // value object
    public class OfferAcceptance : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}