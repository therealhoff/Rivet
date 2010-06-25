using System;
using System.Threading;

namespace Rivet.Scheduler.Impl
{
    public class Seconds : ISchedule
    {
        public Seconds(double seconds)
        {
            _handle = new ManualResetEvent(false);
            _seconds = seconds;
        }

        private WaitHandle _handle;
        private double _seconds;

        public void Wait()
        {
            _handle.WaitOne(TimeSpan.FromSeconds(_seconds));
        }
    }
}