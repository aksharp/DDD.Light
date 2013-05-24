using System;

namespace DDD.Light.Realtor.Core.Domain.Model.Prospect.AggregateRoot
{
    // aggregate root
    public class Prospect : Entity
    {
        public Prospect(Guid id) : base(id)
        {
        }

    }
}