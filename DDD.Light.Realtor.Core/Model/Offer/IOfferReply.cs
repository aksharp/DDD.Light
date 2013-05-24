using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Offer
{
    public interface IOfferReply
    {
        DateTime RepliedOn { get; set; }
    }
}