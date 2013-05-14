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
}
