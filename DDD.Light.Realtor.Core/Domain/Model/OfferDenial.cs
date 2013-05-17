using System;

namespace DDD.Light.Realtor.Core.Domain.Model
{
    // value object
    public class OfferDenial : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}