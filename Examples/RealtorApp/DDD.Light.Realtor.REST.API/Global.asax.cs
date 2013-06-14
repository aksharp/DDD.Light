using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DDD.Light.AggregateStore.InMemory;
using DDD.Light.CQRS.InProcess;
using DDD.Light.EventStore.Contracts;
using DDD.Light.EventStore.MongoDB;
using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.Realtor.REST.API.Bootstrap;
using StructureMap;

namespace DDD.Light.Realtor.REST.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SetUpIoC();

            var mongoStorageStrategy = new MongoStorageStrategy("mongodb://localhost", "DDD_Light_Realtor", "EventStore");
            MongoEventStore.Instance.Configure(mongoStorageStrategy, new JsonEventSerializerStrategy());
            EventBus.Instance.Configure(MongoEventStore.Instance, new JsonEventSerializerStrategy());
            
            InMemoryAggregateStore.Instance.Configure(MongoEventStore.Instance);
            AggregateBus.InProcess.AggregateBus.Instance.Configure(EventBus.Instance);

            InitApp(MongoEventStore.Instance);
        }

        private static void InitApp(IEventStore eventStore)
        {
            HandlerSubscribtions.SubscribeAllHandlers(ObjectFactory.GetInstance);
            CreateRealtorIfNoneExist(eventStore);
        }

        private static void SetUpIoC()
        {
            var container = StructureMapConfig.ConfigureDependencies();
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }

        private static void CreateRealtorIfNoneExist(IEventStore eventStore)
        {
            var realtorId = Guid.Parse("10000000-0000-0000-0000-000000000000");
            if (eventStore.GetById(realtorId) == null)
                CommandBus.Instance.Dispatch(new SetUpRealtor(realtorId));
        }
    }
}