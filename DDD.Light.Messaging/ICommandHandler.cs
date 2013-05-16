namespace DDD.Light.Messaging
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}