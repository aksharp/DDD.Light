using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace DDD.Light.Repo.MongoDB
{
    public sealed class MongoPool
    {
        private static volatile MongoPool _instance;
        private static object token = new Object();
        private readonly Dictionary<string, MongoClient> _mongoClients;

        private MongoPool()
        {
            _mongoClients = new Dictionary<string, MongoClient>();
        }

        public static MongoPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new MongoPool();
                    }
                }
                return _instance;
            }
        }

        public MongoClient GetClient(string connectionString)
        {
            if (!_mongoClients.ContainsKey(connectionString))
            {
                var mongoClient = new MongoClient(connectionString);
                _mongoClients.Add(connectionString, mongoClient);
            }
            return _mongoClients[connectionString];
        }
    }
}
