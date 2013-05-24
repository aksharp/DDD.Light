using System;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.Messaging.Example
{
    public class PersonArrivedEventHandler : IEventHandler<PersonArrivedEvent>
    {
        public void Handle(PersonArrivedEvent personArrivedEvent)
        {
            Console.WriteLine("PersonArrivedEventHandler ::: Handling PersonArrivedEvent... Name: "
                + personArrivedEvent.Name + " Location: " + personArrivedEvent.Location);
        }
    }

    public class PersonLeftEventHandler : IEventHandler<PersonLeftEvent>
    {
        public void Handle(PersonLeftEvent personLeftEvent)
        {
            Console.WriteLine("PersonLeftEventHandler ::: Handling PersonLeftEvent... Name: " 
                + personLeftEvent.Name + " Location: " + personLeftEvent.Location);
        }
    }

    public class PersonLeftAndSpokeEventHandler : IEventHandler<PersonLeftEvent>
    {
        private readonly string _word;

        public PersonLeftAndSpokeEventHandler(string word)
        {
            _word = word;
        }

        public void Handle(PersonLeftEvent personLeftEvent)
        {
            Console.WriteLine("PersonLeftAndSpokeEventHandler ::: Handling PersonLeftEvent... Name: " 
                + personLeftEvent.Name + " Location: " + personLeftEvent.Location + "and said the word: " + _word);
        }
    }

}
