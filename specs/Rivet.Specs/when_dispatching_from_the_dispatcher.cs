using Machine.Specifications;
using Rivet.Broker.Impl;

namespace Rivet.Specs
{
    public class when_dispatching_from_the_dispatcher
    {
        private static Dispatcher Dispatcher;
        private static string Message;

        private Establish that = () =>
                                     {
                                         Dispatcher = new Dispatcher();
                                         Dispatcher.AddAction<string>(msg => Message = msg);
                                     };

        private Because of = () => Dispatcher.Dispatch("Hello World!");

        private It should_have_dispatched_the_appropriate_message = () => Message.ShouldEqual("Hello World!");
    }
}