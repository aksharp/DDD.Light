namespace DDD.Light.Messaging.Example
{
    public class PersonLeftEvent
    {
        public string Name { get;  set; }
        public string Location { get;  set; }

        public PersonLeftEvent()
        {
            
        }
        public PersonLeftEvent(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }

    public class PersonArrivedEvent
    {
        public string Name { get;  set; }
        public string Location { get;  set; }

        public PersonArrivedEvent()
        {
            
        }

        public PersonArrivedEvent(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}
