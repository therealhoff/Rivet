namespace Rivet.Broker
{
    public interface IReply
    {
        void Send<T>(T message);
    }
}