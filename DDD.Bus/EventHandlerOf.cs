namespace DDD.Light.Messaging
{
    public class EventHandlerOf<E>
    {       
        public static bool CanHandle<T>(T @event)
        {
            return !Equals(@event, default(T)) && (typeof(T) == typeof(E));
        }
    }
}