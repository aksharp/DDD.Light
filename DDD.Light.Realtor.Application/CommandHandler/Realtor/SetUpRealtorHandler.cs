using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Command.Realtor;

namespace DDD.Light.Realtor.Application.CommandHandler.Realtor
{
    public class SetUpRealtorHandler : CommandHandler<SetUpRealtor>
    {
        public override void Handle(SetUpRealtor command)
        {
            new Core.Domain.Model.Realtor.Realtor(command.RealtorId);
        }
    }
}
