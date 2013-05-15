using System;

namespace DDD.Light.Messaging.Example
{
    public class PersonArrivedEventHandler : IEventHandler<PersonArrivedEvent>
    {
        public void Handle(PersonArrivedEvent buyerMadeAnOffer)
        {
            Console.WriteLine("PersonArrivedEventHandler ::: Handling PersonArrivedEvent... Name: " 
                + buyerMadeAnOffer.Name + " Location: " + buyerMadeAnOffer.Location);
        }
    }

    public class PersonLeftEventHandler : IEventHandler<PersonLeftEvent>
    {
        public void Handle(PersonLeftEvent buyerMadeAnOffer)
        {
            Console.WriteLine("PersonLeftEventHandler ::: Handling PersonLeftEvent... Name: " 
                + buyerMadeAnOffer.Name + " Location: " + buyerMadeAnOffer.Location);
        }
    }

    public class PersonLeftAndSpokeEventHandler : IEventHandler<PersonLeftEvent>
    {
        private readonly string _word;

        public PersonLeftAndSpokeEventHandler(string word)
        {
            _word = word;
        }

        public void Handle(PersonLeftEvent buyerMadeAnOffer)
        {
            Console.WriteLine("PersonLeftAndSpokeEventHandler ::: Handling PersonLeftEvent... Name: " 
                + buyerMadeAnOffer.Name + " Location: " + buyerMadeAnOffer.Location + "and said the word: " + _word);
        }
    }

}
