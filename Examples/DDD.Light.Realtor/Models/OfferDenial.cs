using System;

namespace DDD.Light.Realtor.Models
{
    public class OfferDenial : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}