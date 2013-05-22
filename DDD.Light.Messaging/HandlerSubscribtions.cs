using System;
using System.Linq;
using DDD.Light.Messaging.Contracts;

namespace DDD.Light.Messaging.InProcess
{
    public class HandlerSubscribtions
    {
        public static void SubscribeAllHandlers(Func<Type, object> getInstance)
        {
            AppDomain.CurrentDomain.GetAssemblies().ToList()
                     .SelectMany(s => s.GetTypes())
                     .Where( t => typeof (IHandler).IsAssignableFrom(t) && t != typeof(IHandler) && t != typeof(CommandHandler<>) && t!= typeof(EventHandler<>))
                     .ToList()
                     .ForEach(t =>
                         {
                             var handler = getInstance.Invoke(t);
                             t.GetMethod("Subscribe").Invoke(handler, null);
                         });
        }               
    }
}