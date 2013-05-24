using System;
using System.Collections.Generic;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.CQRS.InProcess
{
    public class CommandHandlersDatabase<T> : ICommandHandlersDatabase<T>
    {
        private static volatile ICommandHandlersDatabase<T> _instance;
        private static object token = new Object();
        private readonly List<Action<T>> _registeredHandlerActions;

        public static ICommandHandlersDatabase<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new CommandHandlersDatabase<T>();
                    }
                }
                return _instance;
            }
        }

        private CommandHandlersDatabase()
        {
            _registeredHandlerActions = new List<Action<T>>();
        }

        public void Add(ICommandHandler<T> commandHandler)
        {
            _registeredHandlerActions.Add(commandHandler.Handle);
        }

        public void Add(Action<T> commandHandlerAction)
        {
            _registeredHandlerActions.Add(commandHandlerAction);
        }

        public IEnumerable<Action<T>> Get()
        {
            return _registeredHandlerActions;
        }

    }
}