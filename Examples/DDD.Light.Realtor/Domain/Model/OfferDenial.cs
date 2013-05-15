using System;

namespace DDD.Light.Realtor.Domain.Model
{
    // value object
    public class OfferDenial : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}