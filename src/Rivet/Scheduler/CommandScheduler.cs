using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rivet.Scheduler.Impl;

namespace Rivet.Scheduler
{
    public class CommandScheduler
    {
        private readonly IList<IStoppable> _commands;

        public CommandScheduler()
        {
            _commands = new List<IStoppable>();
        }

        public void Add<T>(TimeSpan schedule, Expression<Func<T>> factory) where T : ICommand
        {
            var delay = new Seconds(schedule.TotalSeconds);
            var func = factory.Compile();
            var cmd = new ScheduledCommand<T>(delay, func);
            cmd.Start();
            _commands.Add(cmd);
        }

        public void StopAll(int milliseconds)
        {
            foreach (IStoppable t in _commands)
            {
                t.Stop(milliseconds);
            }
        }
    }
}