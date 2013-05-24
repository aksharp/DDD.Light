using DDD.Light.CQRS.Contracts;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Realtor.API.Query;
using DDD.Light.Realtor.API.Query.Contract;
using DDD.Light.Realtor.API.Query.Model;
using DDD.Light.Realtor.Domain.Model.Buyer;
using DDD.Light.Realtor.Domain.Model.Listing;
using DDD.Light.Realtor.Domain.Model.Offer;
using DDD.Light.Realtor.Domain.Model.Prospect;
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
            const string realtorReadModel = "Realtor_ReadModel";

            IContainer container = ObjectFactory.Container;
            container.Configure(x => x.For<IRepository<Listing>>().Use<MongoRepository<Listing>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Listings")
                );
            container.Configure(x => x.For<IRepository<Buyer>>().Use<MongoRepository<Buyer>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Buyers")
                );
            container.Configure(x => x.For<IRepository<Prospect>>().Use<MongoRepository<Prospect>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Prospects")
                );
            container.Configure(x => x.For<IRepository<Domain.Model.Realtor.Realtor>>().Use<MongoRepository<Domain.Model.Realtor.Realtor>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Realtors")
                );
            container.Configure(x => x.For<IRepository<Offer>>().Use<MongoRepository<Offer>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorDB)
                                      .Ctor<string>("collectionName").Is("Offers")
                );
            container.Configure(x => x.For<IRepository<ActiveListing>>().Use<MongoRepository<ActiveListing>>()
                                      .Ctor<string>("connectionString").Is(mongoConnectionString)
                                      .Ctor<string>("databaseName").Is(realtorReadModel)
                                      .Ctor<string>("collectionName").Is("ActiveListings")
                );
            container.Configure(x => x.For<IActiveListings>().Use<ActiveListings>());
            container.Configure(x => x.For<ICommandBus>().Use(CommandBus.Instance));
            return container;
        }
    }
}