using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;

namespace DDD.Light.Messaging
{
    public class Transaction<T>
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Transaction(T @event, IEnumerable<IEventHandler<T>> eventHandlers)
        {
            Event = @event;
            Id = Guid.NewGuid();
            ProcessedEventHandlers = new List<IEventHandler<T>>();
            ErroredOutEventHandlers = new Dictionary<IEventHandler<T>, Exception>();
            NotProcessedEventHandlers = new Queue<IEventHandler<T>>(eventHandlers);
        }

        public Guid Id { get; private set; }
        public T Event { get; set; }
        public IEnumerable<IEventHandler<T>> ProcessedEventHandlers { get; private set; }
        public IDictionary<IEventHandler<T>, Exception> ErroredOutEventHandlers { get; private set; }
        public Queue<IEventHandler<T>> NotProcessedEventHandlers { get; private set; }

        public void Commit()
        {
            while (NotProcessedEventHandlers.Count > 0)
            {
                var handler = NotProcessedEventHandlers.Dequeue();
                try
                {
                    handler.Handle(Event);
                    ProcessedEventHandlers.ToList().Add(handler);
                }
                catch (Exception ex)
                {
                    ErroredOutEventHandlers.Add(handler, ex);
                }
            }
            LogCommit();
        }

        private void LogCommit()
        {
            var success = NotProcessedEventHandlers.Count == 0 && ErroredOutEventHandlers.Count == 0;
            if (success)
                LogSuccessfulCommit();
            LogFailedCommit();
        }

        private void LogFailedCommit()
        {
            _log.Error(this);
            /*TODO: log failed transaction = 
               1) TransactionId  
             * 2) event type and data, 
             * 3) errored out event handler names and associated exception message and stack trace 
             * 4) not processed event handler names
             * 5) processed handler names
            */
        }

        private void LogSuccessfulCommit()
        {
            _log.Info(this);
            /*TODO: log failed transaction = 
               1) TransactionId 
             * 2) event type and data, 
             * 3) processed handler names
            */
        }
    }
}