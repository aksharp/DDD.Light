using System;

namespace DDD.Light.Realtor.Core.Domain.Events.Listing
{
    public class Deactivated
    {
        public Guid Id { get; private set; }

        public Deactivated(Guid id)
        {
            Id = id;
        }

    }
}