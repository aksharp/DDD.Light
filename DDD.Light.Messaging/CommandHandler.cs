namespace DDD.Light.Messaging
{
    public abstract class CommandHandler<T> : ICommandHandler<T>
    {
        public abstract void Handle(T command);
        public void Subscribe(ICommandBus commandBus)
        {
            commandBus.Subscribe(this);
        }
    }
}