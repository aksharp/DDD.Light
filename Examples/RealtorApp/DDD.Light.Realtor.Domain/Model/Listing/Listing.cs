using System;
using System.Collections.Generic;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Realtor.Domain.Event.Listing;

namespace DDD.Light.Realtor.Domain.Model.Listing
{
    // aggregate root
    public class Listing : AggregateRoot
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

            PublishEvent(new ListingCreated(id, location, description, price));
        }

        // API
        public void Remove()
        {
            PublishEvent(new ListingRemoved(Id));
        }
        
        public void Post()
        {
            PublishEvent(new ListingPosted(
                Id, 
                _description.NumberOfBathrooms, 
                _description.NumberOfBedrooms, 
                _description.YearBuilt,
                _location.Street,
                _location.City,
                _location.State,
                _location.Zip,
                _price)
            );
        }

        // Apply Domain Events to rebuild aggregate
        private void ApplyEvent(ListingRemoved @event)
        {
            _posted = false;
        }
        
        private void ApplyEvent(ListingCreated @event)
        {
            Id = @event.Id;
            _location = @event.Location;
            _description = @event.Description;
            _price = @event.Price;
            _posted = false;
        }
    }
}