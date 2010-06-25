using System;
using System.Collections.Concurrent;

namespace Rivet.Broker.Impl
{
    public class Dispatcher
    {
        public Dispatcher()
        {
            _actions = new ConcurrentDictionary<Type, IActionInvoker>();            
        }

        private readonly ConcurrentDictionary<Type, IActionInvoker> _actions;
        
        public void AddAction<T>(Action<T> action)
        {
            var type = typeof(T);
            _actions.TryAdd(type, new ActionInvoker<T>(action));
        }

        public void Dispatch(object message)
        {
            var type = message.GetType();
            if (_actions.ContainsKey(type))
            {
                _actions[type].Invoke(message);
            }
        }
    }
}