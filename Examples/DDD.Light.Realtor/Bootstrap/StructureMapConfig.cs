using DDD.Light.Realtor.API.Queries;
using DDD.Light.Realtor.Core.Domain.Model;
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
            container.Configure(x => x.For<IRepository<Core.Domain.Model.Realtor>>().Use<MongoRepository<Core.Domain.Model.Realtor>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Realtors")
                );
            container.Configure(x => x.For<IRepository<Offer>>().Use<MongoRepository<Offer>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Offers")
                );
            container.Configure(x => x.For<IListings>().Use<Listings>());
            return container;
        }
    }
}