using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.Realtor.Application;
using DDD.Light.Realtor.REST.API.Bootstrap;
using DDD.Light.Realtor.REST.API.Resources;
using DDD.Light.Repo.Contracts;
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
            ConfigureMappings();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetUpIoC();
            InitApp();
            MongoEventStore.MongoEventStore.Instance.Configure("mongodb://localhost", "DDD.Light.Realtor", "EventStore");
            EventBus.Instance.Configure(MongoEventStore.MongoEventStore.Instance);
        }

        private static void InitApp()
        {
            CreateRealtorIfNoneExist();
            HandlerSubscribtions.SubscribeAllHandlers(ObjectFactory.GetInstance);
        }

        private static void ConfigureMappings()
        {
            Mapper.CreateMap<RealtorListing, PostListing>()
                .ForMember(command => command.ListingId, mapper => mapper.MapFrom(resource => resource.Id));
        }

        private static void SetUpIoC()
        {
            var container = StructureMapConfig.ConfigureDependencies();
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }

        private static void CreateRealtorIfNoneExist()
        {
            var realtorRepo = ObjectFactory.GetInstance<IRepository<Core.Domain.Model.Realtor.Realtor>>();
            if (!realtorRepo.Get().Any())
                realtorRepo.Save(new Core.Domain.Model.Realtor.Realtor {Id = Guid.Empty});
        }
    }
}