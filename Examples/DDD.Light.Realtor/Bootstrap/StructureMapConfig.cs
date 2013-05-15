using DDD.Light.Realtor.Domain.Model;
using DDD.Light.Realtor.Domain.Services;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using StructureMap;

namespace DDD.Light.Realtor.Bootstrap
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
            const string realtorDB = "Realtor";

            IContainer container = ObjectFactory.Container;
            container.Configure(x => x.For<IRepository<Listing>>().Use<MongoRepository<Listing>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Listings")
                );
            container.Configure(x => x.For<IRepository<IBuyer>>().Use<MongoRepository<IBuyer>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Buyers")
                );
            container.Configure(x => x.For<IRepository<Prospect>>().Use<MongoRepository<Prospect>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Prospects")
                );
            container.Configure(x => x.For<IRepository<Domain.Model.Realtor>>().Use<MongoRepository<Domain.Model.Realtor>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Realtors")
                );
            container.Configure(x => x.For<IRepository<Offer>>().Use<MongoRepository<Offer>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Offers")
                );
            container.Configure(x => x.For<IBuyerService>().Use<BuyerService>());
            container.Configure(x => x.For<IProspectService>().Use<ProspectService>());
            return container;
        }
    }
}