namespace Rivet.Broker
{
    public interface IBroker
    {
        void Subscribe<T>(Subscriber<T> subscriber);
        void Publish<T>(T message);
        ICallback Send<T>(T message);
    }
}