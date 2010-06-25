using System;
using System.Collections.Concurrent;
using Rivet.Broker.Impl;

namespace Rivet.Broker
{
    public class MessageBroker : IBroker
    {
        public MessageBroker()
        {
            _subscribers = new ConcurrentDictionary<Type, ParallelCompositeInvoker>();
        }

        private readonly ConcurrentDictionary<Type, ParallelCompositeInvoker> _subscribers;

        public void Subscribe<T>(Subscriber<T> subscriber)
        {
            var invoker = new SubscriberInvoker<T>(subscriber);
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers.TryAdd(type, new ParallelCompositeInvoker());
            }
            _subscribers[type].Add(invoker);
        }

        public void Publish<T>(T message)
        {
            ParallelCompositeInvoker invoker;
            var type = typeof(T);
            
            if (_subscribers.TryGetValue(type, out invoker))
            {
                var callback = new Callback();
                invoker.Invoke(message, callback);
            }
        }

        public ICallback Send<T>(T message)
        {
            ParallelCompositeInvoker invoker;
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out invoker))
            {
                var callback = new Callback();
                invoker.Invoke(message, callback);
                return callback;
            }
            else
            {
                return new NullCallback();
            }
        }
    }

    public delegate void Subscriber<T>(T message, IReply reply);
}