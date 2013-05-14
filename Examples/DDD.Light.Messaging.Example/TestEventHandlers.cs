using System;

namespace DDD.Light.Messaging.Example
{
    public class PersonArrivedEventHandler : IEventHandler<PersonArrivedEvent>
    {
        public void Handle(PersonArrivedEvent e)
        {
            Console.WriteLine("PersonArrivedEventHandler ::: Handling PersonArrivedEvent... Name: " 
                + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftEventHandler : IEventHandler<PersonLeftEvent>
    {
        public void Handle(PersonLeftEvent e)
        {
            Console.WriteLine("PersonLeftEventHandler ::: Handling PersonLeftEvent... Name: " 
                + e.Name + " Location: " + e.Location);
        }
    }

    public class PersonLeftAndSpokeEventHandler : IEventHandler<PersonLeftEvent>
    {
        private readonly string _word;

        public PersonLeftAndSpokeEventHandler(string word)
        {
            _word = word;
        }

        public void Handle(PersonLeftEvent e)
        {
            Console.WriteLine("PersonLeftAndSpokeEventHandler ::: Handling PersonLeftEvent... Name: " 
                + e.Name + " Location: " + e.Location + "and said the word: " + _word);
        }
    }

}
