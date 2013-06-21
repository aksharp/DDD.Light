using System;
using System.Linq;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.InMemory;
using NUnit.Framework;

namespace DDD.Light.CQRS.InProcess.Tests
{
    public class SomeAggregateRoot : AggregateRoot
    {
        private string _message;
        private SomeAggregateRoot(){}

        public SomeAggregateRoot(Guid id, string message) : base(id)
        {
            PublishAndApplyEvent(new SomeAggregateRootCreated(message));
        }
        
        private void ApplyEvent(SomeAggregateRootCreated @event)
        {
            _message = @event.Message;
        }
    }

    public class SomeAggregateRootCreated
    {
        public string Message { get; private set; }

        public SomeAggregateRootCreated(string message)
        {
            Message = message;
        }
    }

    public class SomeAggregateRootCreatedHandler : EventHandler<SomeAggregateRootCreated>
    {
        public override void Handle(SomeAggregateRootCreated @event)
        {
            // no op
        }
    }

    public class MockHandler : IHandler
    {
        public void Subscribe()
        {
            // no op
        }
    }
    
    [TestFixture]
    public class AggregateRootTests
    {
        [Test]
        public void Should_PublishAndApplyEvent_NonGeneric()
        {
            // Arrange
            var serializationStrategy = new JsonEventSerializationStrategy();

            // if you want to use real database like mongo, use next two lines to configure and then use it to configure event store
//            var mongoAggregateEventsRepository = new MongoRepository<AggregateEvent>("mongodb://localhost", "DDD_Light_Tests_EventStore", "EventStore");
//            mongoAggregateEventsRepository.DeleteAll();

            var inMemoryAggregateEventsRepository = new InMemoryRepository<AggregateEvent>();
            inMemoryAggregateEventsRepository.DeleteAll();

            EventStore.EventStore.Instance.Configure(inMemoryAggregateEventsRepository, serializationStrategy);
            EventBus.Instance.Configure(EventStore.EventStore.Instance, serializationStrategy);

            Func<Type, object> getInstance = type => 
                {
                    if (type == typeof (SomeAggregateRootCreatedHandler))
                    {
                        return new SomeAggregateRootCreatedHandler();
                    }
                    if (type == typeof(MockHandler))
                    {
                        return new MockHandler();
                    }
                    if (type == typeof(IRepository<SomeAggregateRoot>))
                    {
                        return new InMemoryRepository<SomeAggregateRoot>();
                    }
                    throw new Exception("type " + type.ToString() + " could not be resolved");
                };
            HandlerSubscribtions.SubscribeAllHandlers(getInstance);

            AggregateCache.AggregateCache.Instance.Configure(EventStore.EventStore.Instance, getInstance);
            AggregateBus.InProcess.AggregateBus.Instance.Configure(EventBus.Instance, AggregateCache.AggregateCache.Instance);

            const string createdMessage = "hello, I am created!";

            var id = Guid.NewGuid();

            // Act
            var ar = new SomeAggregateRoot(id, createdMessage);

            // Assert
            Assert.AreEqual(1, EventStore.EventStore.Instance.Count());
            Assert.AreEqual(typeof(SomeAggregateRootCreated), Type.GetType(EventStore.EventStore.Instance.GetAll().First().EventType));
            Assert.AreEqual(createdMessage, ((SomeAggregateRootCreated)serializationStrategy.DeserializeEvent(EventStore.EventStore.Instance.GetAll().First().SerializedEvent, typeof(SomeAggregateRootCreated))).Message);
        }
    }
}
