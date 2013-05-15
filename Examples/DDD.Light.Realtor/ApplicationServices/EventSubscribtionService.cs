using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.EventHandlers;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.ApplicationServices
{
    public class EventSubscribtionService
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Offer> _offerRepo;

        public EventSubscribtionService(IRepository<Domain.Model.Realtor> realtorRepo, IRepository<Offer> offerRepo)
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
        }

        public void SubscribeEventHandlers()
        {
            EventBus.Instance.Subscribe(new NotifyRealtorOfAnOffer(_realtorRepo));
            EventBus.Instance.Subscribe(new SaveNewOffer(_offerRepo));
        }
    }
}