using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.Light.Repo.Contracts
{
    public interface IRepository<T>
        where T : IEntity
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IQueryable<T> Get();
        void Save(T item);
        void SaveAll(IEnumerable<T> items);
        void Delete(Guid id);
        void Delete(T item);
        void DeleteAll();
    }
}
