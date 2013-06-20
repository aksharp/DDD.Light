using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Repo.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : IEntity
    {
        private static List<T> _db; 

        public InMemoryRepository()
        {
            _db = new List<T>(); 
        }

        public T GetById(Guid id)
        {
            return _db.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _db;
        }

        public IQueryable<T> Get()
        {
            return _db.AsQueryable() ;
        }

        public void Save(T item)
        {
            _db.Add(item);
        }

        public void SaveAll(IEnumerable<T> items)
        {
            items.ToList().ForEach(Save);
        }

        public void Delete(Guid id)
        {
            var item = _db.FirstOrDefault(i => i.Id == id);
            if (!Equals(item, default(T))) 
                Delete(item);
        }

        public void Delete(T item)
        {
            _db.Remove(item);
        }

        public void DeleteAll()
        {
            _db.Clear();
        }

        public long Count()
        {
            return _db.Count;
        }
    }
}
