using System;
using DDD.Light.Realtor.Core.Domain.Event.Offer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Offer
{
    public class OfferMadeHandler : Messaging.InProcess.EventHandler<OfferMade>
    {
        private readonly IRepository<Core.Domain.Model.Realtor.Realtor> _realtorRepo;
        private readonly IRepository<Core.Domain.Model.Offer.Offer> _offerRepo;

        public OfferMadeHandler(IRepository<Core.Domain.Model.Realtor.Realtor> realtorRepo, IRepository<Core.Domain.Model.Offer.Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public override void Handle(OfferMade @event)
        {
  
        }
    }
}