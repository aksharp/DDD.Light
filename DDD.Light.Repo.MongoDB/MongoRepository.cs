using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.Repo.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace DDD.Light.Repo.MongoDB
{
    public class MongoRepository<T> : IRepository<T>
        where T : IEntity 
    {
        private readonly MongoCollection<T> _collection;

        public MongoRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = MongoPool.Instance.GetClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }
 
        public T GetById(Guid id)
        {
            return _collection.FindOneById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.FindAll();
        }
        
        public IQueryable<T> Get()
        {
            return _collection.AsQueryable();
        }

        public void Save(T item)
        {
            _collection.Save(item);
        }

        public void SaveAll(IEnumerable<T> items)
        {
            items.ToList().ForEach(Save);
        }

        public void Delete(Guid id)
        {
            _collection.Remove(Query.EQ("_id", id));
        }

        public void Delete(T item)
        {
            Delete(item.Id);
        }

        public void DeleteAll()
        {
            _collection.RemoveAll();
        }

        public long Count()
        {
            return _collection.Count();
        }
    }
}
