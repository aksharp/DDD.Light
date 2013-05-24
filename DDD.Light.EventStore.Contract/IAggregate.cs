using System;

namespace DDD.Light.EventStore.Contract
{
    public interface IAggregate
    {
        Guid Id { get; }
    }

    public class SomeEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Aggregate : IAggregate
    {
        public Guid Id { get; private set; }

        public void Apply(SomeEvent e)
        {
            
        }
    }
}
