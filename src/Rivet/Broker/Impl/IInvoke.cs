namespace Rivet.Broker.Impl
{
    public interface IInvoke
    {
        void Invoke(object message, Callback callback);
    }
}