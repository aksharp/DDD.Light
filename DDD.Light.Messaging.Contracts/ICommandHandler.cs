namespace DDD.Light.Messaging.Contracts
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}