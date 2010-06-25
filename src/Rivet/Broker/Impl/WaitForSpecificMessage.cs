using System;

namespace Rivet.Broker.Impl
{
    public class WaitForSpecificMessage : IConversation
    {
        public WaitForSpecificMessage(Type type)
        {
            _type = type;
            _complete = false;
        }

        private bool _complete;
        private Type _type;

        public void Peek(object message)
        {
            if (message.GetType().Equals(_type)) _complete = true;
        }

        public bool Complete()
        {
            return _complete;
        }
    }
}