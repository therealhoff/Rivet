using System;

namespace Rivet.Broker.Impl
{
    public class Callback : ICallback, IReply
    {
        private readonly Replies _replies;

        public Callback()
        {
            _replies = new Replies();
        }

        public bool WaitForReply()
        {
            return WaitForReply(2500);
        }

        public bool WaitForReply(int milliseconds)
        {
            HandleReplies(new AnyReply(), milliseconds);
            return true;
        }

        public bool WaitFor<T>()
        {
            return WaitFor<T>(2500);
        }

        public bool WaitForReplies(int count)
        {
            HandleReplies(new WaitForReplyCount(count));
            return true;
        }

        public bool WaitFor<T>(int milliseconds)
        {
            HandleReplies(new WaitForSpecificMessage(typeof(T)), milliseconds);
            return true;
        }

        private void HandleReplies(IConversation pattern)
        {
            HandleReplies(pattern, 2500);
        }

        private void HandleReplies(IConversation pattern, int milliseconds)
        {
            object message;
            while(! pattern.Complete() && _replies.Wait(milliseconds))
            {
                while(! pattern.Complete() && _replies.Dequeue(out message))
                {
                    pattern.Peek(message);
                    _replies.Dispatch(message);
                }
            }
        }

        public ICallback On<T>(Action<T> action)
        {
            _replies.AddAction(action);
            return this;
        }

        void IReply.Send<T>(T message)
        {
            _replies.Send(message);
        }
    }
}