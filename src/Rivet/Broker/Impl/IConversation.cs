namespace Rivet.Broker.Impl
{
    public interface IConversation
    {
        void Peek(object message);
        bool Complete();
    }
}