using System;

namespace DDD.Light.Messaging.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = EventBus.Instance;
            bus.Subscribe(new PersonLeftEventHandler());
            bus.Subscribe(new PersonLeftEventHandler2("good bye"));
            bus.Subscribe(new PersonArrivedEventHandler());

            bus.Publish(new PersonLeftEvent("Jane Doe", "California"));
            bus.Publish<PersonLeftEvent>(null);
            bus.Publish(new PersonArrivedEvent("Jane Doe", "New York"));
            
            Console.ReadLine();

        }
    }
}
