using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rivet.Scheduler.Impl
{
    public class ScheduledCommand<T> : IStoppable where T : ICommand
    {
        private readonly Func<T> _factory;
        private readonly ISchedule _schedule;
        private bool _run;
        private readonly Task _task;

        public ScheduledCommand(ISchedule schedule, Func<T> factory)
        {
            _schedule = schedule;
            _factory = factory;
            _run = false;
            _task = new Task(Loop);
        }

        public void Start()
        {
            _run = true;
            _task.Start();
        }

        private void Loop()
        {
            while (_run)
            {
                _schedule.Wait();
                _factory().Execute();
            }
        }

        public void Stop(int milliseconds)
        {
            _run = false;
            _task.Wait(milliseconds);
        }
    }
}