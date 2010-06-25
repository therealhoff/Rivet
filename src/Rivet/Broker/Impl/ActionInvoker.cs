using System;

namespace Rivet.Broker.Impl
{
    public class ActionInvoker<T> : IActionInvoker
    {
        private Action<T> _action;

        public ActionInvoker(Action<T> action)
        {
            _action = action;
        }

        public void Invoke(object message)
        {
            _action((T) message);
        }
    }

    public interface IActionInvoker
    {
        void Invoke(object message);
    }
}