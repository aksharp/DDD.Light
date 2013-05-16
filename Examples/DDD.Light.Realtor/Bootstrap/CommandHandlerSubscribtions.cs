using DDD.Light.Messaging;
using DDD.Light.Realtor.Application.CommandHandlers.Realtor;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Bootstrap
{
    public class CommandHandlerSubscribtions
    {
        private readonly IRepository<Domain.Model.Realtor> _realtorRepo;

        public CommandHandlerSubscribtions(IRepository<Domain.Model.Realtor> realtorRepo)
        {
            _realtorRepo = realtorRepo;
        }

        public void SubscribeCommandHandlers()
        {
            CommandBus.Instance.Subscribe(new PostListingHandler(_realtorRepo));
        }
    }
}