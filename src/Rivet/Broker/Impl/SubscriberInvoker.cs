using System.Threading.Tasks;

namespace Rivet.Broker.Impl
{
    public class SubscriberInvoker<T> : IInvoke
    {
        private readonly Subscriber<T> _subscriber;

        public SubscriberInvoker(Subscriber<T> action)
        {
            _subscriber = action;
        }

        #region IInvoke Members

        public void Invoke(object message, Callback callback)
        {
            var task = new Task(() => _subscriber((T) message, callback));
            task.ContinueWith(Complete);
            task.Start();
        }

        private void Complete(Task task)
        {
            task.Dispose();
        }

        #endregion
    }
}