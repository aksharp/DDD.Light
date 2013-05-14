using DDD.Light.Realtor.Models;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using StructureMap;

namespace DDD.Light.Realtor
{
    public static class StructureMapContainer
    {
        public static IContainer ConfigureDependencies()
        {
            IContainer container = new Container();
            container.Configure(x => x.For<IRepository<Listing>>().Use<MongoRepository<Listing>>()
                                      .Ctor<string>("connectionString").Is("mongodb://localhost")
                                      .Ctor<string>("databaseName").Is("db")
                                      .Ctor<string>("collectionName").Is("col")
                );
            return container;
        }
    }
}