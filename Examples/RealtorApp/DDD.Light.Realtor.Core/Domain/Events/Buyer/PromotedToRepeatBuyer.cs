using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot;

namespace DDD.Light.Realtor.Core.Domain.Events.Buyer
{
    public class PromotedToRepeatBuyer
    {
        public Model.Buyer.AggregateRoot.Buyer Buyer { get; set; }
        public RepeatBuyer RepeatBuyer { get; set; }
    }
}