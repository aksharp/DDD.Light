using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.Realtor.Application.CommandHandler.Realtor
{
    public class SetUpRealtorHandler : CommandHandler<SetUpRealtor>
    {
        public override void Handle(SetUpRealtor command)
        {
            new Domain.Model.Realtor.Realtor(command.RealtorId);
        }
    }
}
