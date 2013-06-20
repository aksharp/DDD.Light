using System;
using System.Linq;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.CQRS.InProcess
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
                             object handler;
                             try
                             {
                                 handler = getInstance.Invoke(t);
                             }
                             catch (Exception ex)
                             {
                                 throw new Exception(string.Format("SubscribeAllHandlers could not getInstance of type {0} resulting in esception message {1}", t, ex.Message));
                             }
                             
                             try
                             {
                                 t.GetMethod("Subscribe").Invoke(handler, null);
                             }
                             catch (Exception ex)
                             {
                                 throw new Exception(string.Format("SubscribeAllHandlers could not invoke Subscribe method on type {0} resulting in esception message {1}", t, ex.Message));
                             }

                         });
        }               
    }
}