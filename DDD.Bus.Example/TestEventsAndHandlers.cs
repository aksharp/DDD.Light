using System;

namespace DDD.Light.Messaging.Example
{
    public class PersonLeftEvent
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public PersonLeftEvent(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }

    public class PersonArrivedEvent
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public PersonArrivedEvent(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }

    public class PersonArrivedEventHandler : IEventHandler
    {
        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonArrivedEvent>.CanHandle(@event)) return;
            var e = @event as PersonArrivedEvent;
            Console.WriteLine("PersonArrivedEventHandler ::: Handling PersonArrivedEvent... Name: " + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftEventHandler : IEventHandler
    {
        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonLeftEvent>.CanHandle(@event)) return;
            var e = @event as PersonLeftEvent;
            Console.WriteLine("PersonLeftEventHandler ::: Handling PersonLeftEvent... Name: " + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftEventHandler2 : IEventHandler
    {
        private readonly string _word;

        public PersonLeftEventHandler2(string word)
        {
            _word = word;
        }

        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonLeftEvent>.CanHandle(@event)) return;
            var e = @event as PersonLeftEvent;
            Console.WriteLine("PersonLeftEventHandler2 ::: Handling PersonLeftEvent... Name: " + e.Name + " Location: " +
                                  e.Location + "and said the word: " + _word);
        }
    }

}
