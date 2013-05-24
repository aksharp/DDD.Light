using System;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Events.Listing;

namespace DDD.Light.Realtor.Core.Domain.Model.Listing.AggregateRoot
{
    // aggregate root
    public class Listing : Entity
    {
        private Location _location;
        private Description _description;
        private decimal _price;
        private bool _active;

        public Listing(Guid id, Location location, Description description, decimal price, bool active) : base(id)
        {
            _location = location;
            _description = description;
            _price = price;
            _active = active;

            EventBus.Instance.Publish(active ? new Published(this) : new Staged(this));
        }

        public void Apply(Described described)
        {
            _description = described.Description;
        }

        public void Apply(Deactivated deactivated)
        {
            _active = false;
        }

        public void Describe(Description description)
        {
            _description = description;
            EventBus.Instance.Publish(new Described(Id, description));
        }

        public void Deactivate()
        {
            _active = false;
            EventBus.Instance.Publish(new Deactivated{Listing = this});
        }
    }
}