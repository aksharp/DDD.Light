using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Events
{
    public class BuyerPromotedToRepeatBuyer
    {
        public Buyer Buyer { get; set; }
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}