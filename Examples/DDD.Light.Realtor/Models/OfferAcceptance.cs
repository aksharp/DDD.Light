using System;

namespace DDD.Light.Realtor.Models
{
    public class OfferAcceptance : IOfferReply
    {
        public DateTime RepliedOn { get; set; }
    }
}