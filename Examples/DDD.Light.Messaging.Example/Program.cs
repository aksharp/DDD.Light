using System;

namespace DDD.Light.Messaging.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // subscribe event handlers to handle events
            // in real life subscription would occur on app start, in Global.asax.cs on web apps
            // handle method in real life would call method(s) on aggregate root entity
            EventBus.Instance.Subscribe(new PersonLeftEventHandler());
            EventBus.Instance.Subscribe(new PersonLeftAndSpokeEventHandler("good bye"));
            EventBus.Instance.Subscribe(new PersonArrivedEventHandler());

            // publish events to state something was done
            // events in real life would be published from methods in aggregate root entity
            EventBus.Instance.Publish(new PersonLeftEvent("Jane Doe", "California"));
            EventBus.Instance.Publish<PersonLeftEvent>(null);
            EventBus.Instance.Publish(new PersonArrivedEvent("Jane Doe", "New York"));
            
            Console.ReadLine();

        }
    }
}
