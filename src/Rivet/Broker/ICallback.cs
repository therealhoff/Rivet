using System;

namespace Rivet.Broker
{
    public interface ICallback
    {
        bool WaitForReply();
        bool WaitForReply(int milliseconds);
        bool WaitFor<T>();
        bool WaitForReplies(int count);
        ICallback On<T>(Action<T> action);
    }
}