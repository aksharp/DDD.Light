using System;
using System.Collections.Generic;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Listing;

namespace DDD.Light.Realtor.Core.Domain.Model.Listing
{
    // aggregate root
    public class Listing : Entity
    {
        private Location _location;
        private Description _description;
        private decimal _price;
        private bool _posted;
        private IEnumerable<Guid> _offers;

        private Listing(){}

        public Listing(Guid id, Location location, Description description, decimal price) : base(id)
        {
            _location = location;
            _description = description;
            _price = price;
            _posted = false;

            PublishEvent(new ListingCreated(this));
        }

        // API
        public void Remove()
        {
            PublishEvent(new ListingRemoved(Id));
        }

        // Apply Domain Events to rebuild aggregate
        private void ApplyEvent(ListingRemoved @event)
        {
            _posted = false;
        }
    }
}