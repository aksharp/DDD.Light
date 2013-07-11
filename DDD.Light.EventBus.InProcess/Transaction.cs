using System;
using System.Collections.Generic;

namespace DDD.Light.EventBus.InProcess
{
    public class Transaction<TEvent>
    {
        public Transaction(){}
        public Transaction(TEvent message, IEnumerable<Action<TEvent>> handlers)
        {
            Message = message;
            Id = Guid.NewGuid();
            ProcessedActions = new List<Action<TEvent>>();
            ErroredOutActions = new Dictionary<Action<TEvent>, Exception>();
            NotProcessedActions = new Queue<Action<TEvent>>(handlers);
        }

        public Guid Id { get; private set; }
        public TEvent Message { get; set; }
        public List<Action<TEvent>> ProcessedActions { get; private set; }
        public IDictionary<Action<TEvent>, Exception> ErroredOutActions { get; private set; }
        public Queue<Action<TEvent>> NotProcessedActions { get; private set; }

        public void Commit()
        {
            while (NotProcessedActions.Count > 0)
            {
                var handler = NotProcessedActions.Dequeue();
                try
                {
                    handler.Invoke(Message);
                    ProcessedActions.Add(handler);
                }
                catch (Exception ex)
                {
                    ErroredOutActions.Add(handler, ex);
                    throw;
                }
            }
        }

    }
}