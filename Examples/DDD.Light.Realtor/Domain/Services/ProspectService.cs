using System;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Services
{
    public class ProspectService : IProspectService
    {
        private readonly IRepository<Prospect> _prospectRepo;
        private readonly IRepository<IBuyer> _buyerRepo;

        public ProspectService(IRepository<Prospect> prospectRepo, IRepository<IBuyer> buyerRepo)
        {
            _prospectRepo = prospectRepo;
            _buyerRepo = buyerRepo;
        }

        public void MakeAnOffer(Guid prospectId, Guid listingId, decimal price)
        {
            var buyer = PromoteProspectToBuyer(prospectId);
            buyer.MakeAnOffer(listingId, price);
        }

        public Buyer PromoteProspectToBuyer(Guid prospectId)
        {
            var prospect = _prospectRepo.GetById(prospectId);
            var buyer = new Buyer
            {
                Id = Guid.NewGuid(),
                Prospect = prospect
            };
            _prospectRepo.Delete(prospect);
            _buyerRepo.Save(buyer);
            return buyer;
        }
    }
}