using DDD.Light.Realtor.Domain.Event.Offer;
using DDD.Light.Repo.Contracts;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.EventHandler.Offer
{
    public class OfferMadeHandler : EventHandler<OfferMade>
    {
        private readonly IRepository<Domain.Model.Realtor.Realtor> _realtorRepo;
        private readonly IRepository<Domain.Model.Offer.Offer> _offerRepo;

        public OfferMadeHandler(IRepository<Domain.Model.Realtor.Realtor> realtorRepo, IRepository<Domain.Model.Offer.Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public override void Handle(OfferMade @event)
        {
  
        }
    }
}