using System;
using Machine.Specifications;
using Rivet.Scheduler.Impl;

namespace Rivet.Specs
{
    public class when_waiting_on_a_schedule
    {
        private static ISchedule _schedule;
        private static DateTime _start;
        private static DateTime _end;

        private Because of = () =>
                                 {
                                     _start = DateTime.Now;
                                     _schedule.Wait();
                                     _end = DateTime.Now;
                                 };

        private It should_pause_the_desired_number_of_seconds =
            () => (_start <= _end.Subtract(TimeSpan.FromSeconds(3))).ShouldBeTrue();

        private Establish that = () => _schedule = new Seconds(3);
    }
}