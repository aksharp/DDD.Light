﻿using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Offer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandler.Offer
{
    public class AcceptedHandler : EventHandler<Accepted>
    {
        private readonly IRepository<Core.Domain.Model.Buyer.Buyer> _buyerRepo;

        public AcceptedHandler(IRepository<Core.Domain.Model.Buyer.Buyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(Accepted @event)
        {
            var buyer = _buyerRepo.GetById(@event.Offer.BuyerId);
            buyer.NotifyOfRejectedOffer(@event.Offer);

        }
    }
}