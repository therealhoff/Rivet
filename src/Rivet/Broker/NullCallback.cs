using System;

namespace Rivet.Broker
{
    public class NullCallback : ICallback
    {
        public bool WaitForReply()
        {
            return false;
        }

        public bool WaitForReply(int milliseconds)
        {
            return false;
        }

        public bool WaitFor<T>()
        {
            return false;
        }

        public bool WaitForReplies(int count)
        {
            return false;
        }

        public ICallback On<T>(Action<T> action)
        {
            return this;
        }
    }
}