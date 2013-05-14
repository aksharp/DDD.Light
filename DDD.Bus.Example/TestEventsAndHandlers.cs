using System;

namespace DDD.Light.Messaging.Example
{
    public class PersonArrivedEventHandler : IEventHandler
    {
        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonArrivedEvent>.CanHandle(@event)) return;
            var e = @event as PersonArrivedEvent;
            Console.WriteLine("PersonArrivedEventHandler ::: Handling PersonArrivedEvent... Name: " 
                + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftEventHandler : IEventHandler
    {
        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonLeftEvent>.CanHandle(@event)) return;
            var e = @event as PersonLeftEvent;
            Console.WriteLine("PersonLeftEventHandler ::: Handling PersonLeftEvent... Name: " 
                + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftAndSpokeEventHandler : IEventHandler
    {
        private readonly string _word;

        public PersonLeftAndSpokeEventHandler(string word)
        {
            _word = word;
        }

        public void Handle<T>(T @event)
        {
            if (!EventHandlerOf<PersonLeftEvent>.CanHandle(@event)) return;
            var e = @event as PersonLeftEvent;
            Console.WriteLine("PersonLeftAndSpokeEventHandler ::: Handling PersonLeftEvent... Name: " 
                + e.Name + " Location: " + e.Location + "and said the word: " + _word);
        }
    }

}
