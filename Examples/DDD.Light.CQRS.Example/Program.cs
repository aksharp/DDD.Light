using System;
using System.Reflection;
using DDD.Light.CQRS.InProcess;
using DDD.Light.EventStore;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.MongoDB;
using log4net;
using log4net.Config;

namespace DDD.Light.Messaging.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            EventStore.EventStore.Instance.Configure(new MongoRepository<AggregateEvent>("mongodb://localhost", "DDD_Light_Messaging_Example", "EventStore"), new JsonEventSerializationStrategy());
            EventBus.Instance.Configure(EventStore.EventStore.Instance, new JsonEventSerializationStrategy());


            log.Info("------- START ---------");

            // subscribe event handlers to handle events
            // in real life subscription would occur on app start, in Global.asax.cs on web apps
            // handle method in real life would call method(s) on aggregate root entity
            EventBus.Instance.Subscribe(new PersonLeftEventHandler());
            EventBus.Instance.Subscribe(new PersonLeftAndSpokeEventHandler("good bye"));
            EventBus.Instance.Subscribe(new PersonArrivedEventHandler());

            //todo: refer to RealtorApp for a CQRS example. Might update this one later
            // publish events to state something was done
            // events in real life would be published from methods in aggregate root entity
//            var Id = Guid.NewGuid();
//            EventBus.Instance.Publish(Id, new PersonLeftEvent("Jane Doe", "California"));
//            EventBus.Instance.Publish<PersonLeftEvent>(Id, null);
//            EventBus.Instance.Publish(Id, new PersonArrivedEvent("Jane Doe", "New York"));

            log.Info("------- END ---------");

            Console.ReadLine();

        }
    }
}
