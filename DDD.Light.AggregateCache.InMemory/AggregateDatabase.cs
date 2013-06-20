using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Light.AggregateCache.Contracts;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateCache.InMemory
{
    public class AggregateDatabase<T> : IAggregateDatabase<T> where T : IAggregateRoot
    {
        private static volatile IAggregateDatabase<T> _instance;
        private static object token = new Object();
        private readonly List<T> _aggregates;

        public static IAggregateDatabase<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new AggregateDatabase<T>();
                    }
                }
                return _instance;
            }
        }

        private AggregateDatabase()
        {
            _aggregates = new List<T>();
        }

        public void Add(T aggregate)
        {
            if (_aggregates.Any(a => a.Id == aggregate.Id)) return;
            _aggregates.Add(aggregate);
        }

        public T GetById(Guid aggregateId)
        {
            return _aggregates.FirstOrDefault(a => a.Id == aggregateId);
        }
    }
}