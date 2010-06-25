namespace Rivet.Broker.Impl
{
    public class AnyReply : IConversation
    {
        public AnyReply()
        {
            _complete = false;
        }

        private bool _complete;


        public void Peek(object message)
        {
            _complete = true;
        }

        public bool Complete()
        {
            return _complete;
        }
    }
}