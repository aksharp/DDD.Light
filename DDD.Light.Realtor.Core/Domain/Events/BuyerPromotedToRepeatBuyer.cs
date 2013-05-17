using DDD.Light.Realtor.Core.Domain.Model;

namespace DDD.Light.Realtor.Core.Domain.Events
{
    public class BuyerPromotedToRepeatBuyer
    {
        public Buyer Buyer { get; set; }
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}