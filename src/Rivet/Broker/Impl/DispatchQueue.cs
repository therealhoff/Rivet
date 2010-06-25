using System.Collections.Concurrent;
using System.Threading;

namespace Rivet.Broker.Impl
{
    public class DispatchQueue
    {
        public DispatchQueue()
        {
            _replies = new ConcurrentQueue<object>();
            _reset = new AutoResetEvent(false);
        }

        private readonly ConcurrentQueue<object> _replies;
        private readonly AutoResetEvent _reset;

        public void Enqueue<T>(T message)
        {
            _replies.Enqueue(message);
            _reset.Set();
        }

        public bool Dequeue(out object message)
        {
            return _replies.TryDequeue(out message);
        }

        public bool WaitOne(int milliseconds)
        {
            return _reset.WaitOne(milliseconds);
        }
    }
}