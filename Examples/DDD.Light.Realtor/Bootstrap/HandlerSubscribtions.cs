using System;
using System.Linq;
using System.Reflection;
using DDD.Light.Messaging;
using StructureMap;

namespace DDD.Light.Realtor.Bootstrap
{
    public class HandlerSubscribtions
    {
        public static void SubscribeEventHandlersInNamespace(string @namespace)
        {
            var handlerTypes = GetTypesInNamespace(Assembly.GetExecutingAssembly(), @namespace);
            foreach (var t in handlerTypes)
            {
                var handler = ObjectFactory.GetInstance(t);
                t.GetMethod("Subscribe").Invoke(handler, null);
            }
        }
        
        public static void SubscribeCommandHandlersInNamespace(string @namespace)
        {
            var handlerTypes = GetTypesInNamespace(Assembly.GetExecutingAssembly(), @namespace);
            foreach (var t in handlerTypes)
            {
                var handler = ObjectFactory.GetInstance(t);
                t.GetMethod("Subscribe").Invoke(handler, null);
            }
        }
        
        public static void SubscribeAllHandlers()
        {
            AppDomain.CurrentDomain.GetAssemblies().ToList()
                     .SelectMany(s => s.GetTypes())
                     .Where( t => typeof (IHandler).IsAssignableFrom(t) && t != typeof(IHandler) && t != typeof(CommandHandler<>) && t!= typeof(Messaging.EventHandler<>))
                     .ToList()
                     .ForEach(t =>
                         {
                             var handler = ObjectFactory.GetInstance(t);
                             t.GetMethod("Subscribe").Invoke(handler, null);
                         });
        }
        
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
        
    }
}