using DDD.Light.Realtor.Core.Domain.Model.Buyer;

namespace DDD.Light.Realtor.Core.Domain.Events.Buyer
{
    public class PromotedToRepeatBuyer
    {
        public Model.Buyer.Buyer Buyer { get; set; }
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}