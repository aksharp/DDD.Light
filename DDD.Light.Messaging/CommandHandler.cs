using DDD.Light.Messaging.Contracts;

namespace DDD.Light.Messaging.InProcess
{
    public abstract class CommandHandler<T> : ICommandHandler<T>, IHandler
    {
        public abstract void Handle(T command);
        public void Subscribe()
        {
            CommandBus.Instance.Subscribe(this);
        }
    }
}