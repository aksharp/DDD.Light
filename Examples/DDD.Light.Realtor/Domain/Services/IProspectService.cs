using System;
using DDD.Light.Realtor.Domain.Model;

namespace DDD.Light.Realtor.Domain.Services
{
    public interface IProspectService
    {
        void MakeAnOffer(Guid prospectId, Guid listingId, decimal price);
        Buyer PromoteProspectToBuyer(Guid prospectId);
    }
}