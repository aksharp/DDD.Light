using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.EventHandlers
{
    public class NotifyRealtorOfAnOffer : IEventHandler<BuyerMadeAnOffer>
    {
        private readonly IRepository<Model.Realtor> _realtorRepository;

        public NotifyRealtorOfAnOffer(IRepository<Model.Realtor> realtorRepository)
        {
            _realtorRepository = realtorRepository;
        }

        public void Handle(BuyerMadeAnOffer buyerMadeAnOffer)
        {
            var realtor = _realtorRepository.GetById(Guid.Empty);
            realtor.NotifyThatOfferWasMade(buyerMadeAnOffer.OfferId);
            _realtorRepository.Save(realtor);
        }
    }
}