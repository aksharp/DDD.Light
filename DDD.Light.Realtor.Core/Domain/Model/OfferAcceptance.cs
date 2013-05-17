using System;

namespace DDD.Light.Realtor.Core.Domain.Model
{
    // value object
    public class OfferAcceptance : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}