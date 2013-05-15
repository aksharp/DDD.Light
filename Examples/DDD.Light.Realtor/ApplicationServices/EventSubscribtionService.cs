using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.EventHandlers;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Realtor.Domain.Services;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.ApplicationServices
{
    public class EventSubscribtionService
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Offer> _offerRepo;
        private readonly IRepository<IBuyer> _buyerRepo;
        private readonly IRepository<Listing> _listingRepo;
        private readonly IBuyerService _buyerService;

        public EventSubscribtionService(
            IRepository<Domain.Model.Realtor> realtorRepo, 
            IRepository<Offer> offerRepo, 
            IRepository<IBuyer> buyerRepo,
            IRepository<Listing> listingRepo,
            IBuyerService buyerService)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
            _buyerRepo = buyerRepo;
            _listingRepo = listingRepo;
            _buyerService = buyerService;
        }

        public void SubscribeEventHandlers()
        {
            EventBus.Instance.Subscribe(new NotifyRealtorOfAnOffer(_realtorRepo));
            EventBus.Instance.Subscribe(new PersistNewOffer(_offerRepo));
            EventBus.Instance.Subscribe(new PersistAcceptedOffer(_offerRepo));
            EventBus.Instance.Subscribe(new ConvertListingToBuyersProperty(_buyerRepo, _buyerService, _listingRepo));
            EventBus.Instance.Subscribe(new PersistDeniedOffer(_offerRepo));
            EventBus.Instance.Subscribe(new DeactivateListing(_listingRepo));
        }
    }
}