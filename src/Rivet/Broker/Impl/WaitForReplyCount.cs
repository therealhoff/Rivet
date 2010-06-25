namespace Rivet.Broker.Impl
{
    public class WaitForReplyCount : IConversation
    {
        private readonly int _count;
        private int _received;

        public WaitForReplyCount(int count)
        {
            _count = count;
            _received = 0;
        }


        public void Peek(object message)
        {
            _received++;
        }

        public bool Complete()
        {
            return _count <= _received;
        }
    }
}