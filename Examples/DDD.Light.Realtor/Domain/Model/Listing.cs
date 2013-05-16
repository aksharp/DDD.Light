using System;
using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Realtor.Domain.Events;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Listing : Entity
    {
        public Listing()
        {
            Location = new Location();
            Description = new Description();
        }

        public Location Location { get; set; }
        public Description Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Guid> Offers { get; set; }
        public bool Active { get; set; }

        public void Deactivate()
        {
            Active = false;
            EventBus.Instance.Publish(new ListingDeactivated{Listing = this});
        }
    }
}