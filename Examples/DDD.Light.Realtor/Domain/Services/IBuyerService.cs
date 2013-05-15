using System;
using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Services
{
    public interface IBuyerService
    {
        RepeatBuyer PromoteBuyerToRepeatBuyer(Guid buyerId);
    }
}