using DDD.Light.Messaging;
using DDD.Light.Realtor.Application.EventHandlers.Buyers;
using DDD.Light.Realtor.Application.EventHandlers.Listings;
using DDD.Light.Realtor.Application.EventHandlers.Offers;
using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Bootstrap
{
    public class EventHandlerSubscribtions
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;
        private readonly IRepository<Offer> _offerRepo;
        private readonly IRepository<IBuyer> _buyerRepo;
        private readonly IRepository<Listing> _listingRepo;
        

        public EventHandlerSubscribtions(
            IRepository<Domain.Model.Realtor> realtorRepo, 
            IRepository<Offer> offerRepo, 
            IRepository<IBuyer> buyerRepo,
            IRepository<Listing> listingRepo
            )
        {
            _realtorRepo = realtorRepo;
            _offerRepo = offerRepo;
            _buyerRepo = buyerRepo;
            _listingRepo = listingRepo;

        }

        public void SubscribeEventHandlers()
        {
            EventBus.Instance.Subscribe(new OfferAcceptedHandler(_buyerRepo));
            EventBus.Instance.Subscribe(new OfferRejectedHandler(_offerRepo, _buyerRepo));
            EventBus.Instance.Subscribe(new BuyerPromotedToRepeatBuyerHandler(_buyerRepo));
            EventBus.Instance.Subscribe(new ListingDeactivatedHandler(_listingRepo));
            EventBus.Instance.Subscribe(new ListingPostedHandler(_realtorRepo,_listingRepo));
        }
    }
}