using DDD.Light.CQRS.Contracts;
using DDD.Light.CQRS.InProcess;
using DDD.Light.EventStore.Contracts;
using DDD.Light.EventStore.MongoDB;
using DDD.Light.Realtor.API.Query;
using DDD.Light.Realtor.API.Query.Contract;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using StructureMap;

namespace DDD.Light.Realtor.REST.API.Bootstrap
{
    public static class StructureMapConfig
    {
        public static IContainer ConfigureDependencies()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                }));

            const string mongoConnectionString = "mongodb://localhost";
            const string realtorReadModel = "DDD_Light_Realtor_ReadModel";

            IContainer container = ObjectFactory.Container;
            container.Configure(x => x.For<IRepository<Listing>>().Use<MongoRepository<Listing>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorReadModel)
                                      .Ctor<string>("collectionName").Is("Listings")
                );
            container.Configure(x => x.For<IActiveListings>().Use<ActiveListings>());
            container.Configure(x => x.For<ICommandBus>().Use(CommandBus.Instance));
            container.Configure(x => x.For<IEventStore>().Use(MongoEventStore.Instance));
            return container;
        }
    }
}