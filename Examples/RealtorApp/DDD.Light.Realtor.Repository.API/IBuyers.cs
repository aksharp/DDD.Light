using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract;

namespace DDD.Light.Realtor.Repository.API
{
    interface IBuyers
    {
        IBuyer GetById(Guid id);
        IEnumerable<IBuyer> GetAll();
        IQueryable<IBuyer> Get();
        void Save(IBuyer item);
        void SaveAll(IEnumerable<IBuyer> items);
        void Delete(Guid id);
        void Delete(IBuyer item);
        void DeleteAll();
    }
}
