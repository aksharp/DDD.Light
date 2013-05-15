using System;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public BuyerService(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public RepeatBuyer PromoteBuyerToRepeatBuyer(Guid buyerId)
        {
            var buyer = _buyerRepo.GetById(buyerId) as Buyer;
            if (buyer == null) throw new Exception("No buyer with buyerId" + buyerId + " found");
            var repeatBuyer = new RepeatBuyer
            {
                Id = buyer.Id,
                OfferIds = buyer.OfferIds,
                Prospect = buyer.Prospect
            };           
            _buyerRepo.Delete(buyer);
            _buyerRepo.Save(repeatBuyer);
            return repeatBuyer;
        }
    }
}