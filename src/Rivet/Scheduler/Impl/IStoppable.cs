namespace Rivet.Scheduler.Impl
{
    public interface IStoppable : IStartable
    {
        void Stop(int milliseconds);
    }
}