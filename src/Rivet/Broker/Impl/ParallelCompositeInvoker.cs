using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Rivet.Broker.Impl
{
    public class ParallelCompositeInvoker : IInvoke
    {
        public ParallelCompositeInvoker()
        {
            _subscribers = new ConcurrentBag<IInvoke>();
        }

        private readonly ConcurrentBag<IInvoke> _subscribers;

        public void Add(IInvoke invoker)
        {
            _subscribers.Add(invoker);
        }

        public void Invoke(object message, Callback callback)
        {
            var invokers = new List<IInvoke>(_subscribers);
            foreach (var invoker in invokers)
            {
                invoker.Invoke(message, callback);
            }
        }
    }
}