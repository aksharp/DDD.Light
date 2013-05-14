using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DDD.Light.Realtor.Controllers;
using DDD.Light.Realtor.Models;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace DDD.Light.Realtor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801



    /// <summary>
    /// Wrapper for IDependencyScope, so that StructureMap plays nicely with built in mvc4 dependency resolution.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase, IDependencyScope
    {
        protected readonly IContainer Container;

        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                         ? Container.TryGetInstance(serviceType)
                         : Container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        ///        When implemented by inheriting classes, this method will do the actual work of resolving
        ///        the requested service instance.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Container.GetInstance(serviceType);
            }
            return Container.GetInstance(serviceType, key);
        }

        /// <summary>
        ///        When implemented by inheriting classes, this method will do the actual work of
        ///        resolving all the requested service instances.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>
        /// Sequence of service instance objects.
        /// </returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for IDependencyResolver so that StructureMap plays nicely with built in mvc 4 dependency resolution. 
    /// </summary>
    public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.GetNestedContainer();
            return new StructureMapDependencyResolver(child);
        }
    }



    class SimpleContainer : IDependencyResolver
    {
        static readonly IRepository<Listing> respository = new MongoRepository<Listing>("mongodb://localhost", "db", "collection");

        public IDependencyScope BeginScope()
        {
            // This example does not support child scopes, so we simply return 'this'.
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(ListingsController))
            {
                return new ListingsController(respository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }
    }

//    public class StructureMapDependencyResolver : IDependencyResolver
//    {
//        private readonly IContainer _container;
//
//        public StructureMapDependencyResolver(IContainer container)
//        {
//            _container = container;
//        }
//
//        public object GetService(Type serviceType)
//        {
//            if (serviceType.IsAbstract || serviceType.IsInterface)
//            {
//
//                return _container.TryGetInstance(serviceType);
//
//            }
//
//            return _container.GetInstance(serviceType);
//        }
//
//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            return _container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
//        }
//
//        public IDependencyScope BeginScope()
//        {
//            return this;
//        }
//
//        public void Dispose()
//        {
//            
//        }
//    }




    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

//            GlobalConfiguration.Configuration.DependencyResolver = new SimpleContainer();

            var container = ConfigureDependencies();
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }

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