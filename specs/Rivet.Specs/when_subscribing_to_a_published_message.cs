using System;
using System.Threading;
using Machine.Specifications;
using Rivet.Broker;

namespace Rivet.Specs
{
    public class when_subscribing_to_a_published_message
    {
        private static ManualResetEvent Handle;
        private static Subscriber<object> Subscription;
        private static IBroker Broker;
        private static object Sent;
        private static object Received;

        private Establish that = () =>
                                     {
                                         Sent = new object();
                                         Handle = new ManualResetEvent(false);
                                         Subscription = (object msg, IReply reply) =>
                                                            {
                                                                Received = msg;
                                                                Handle.Set();
                                                            };
                                         Broker = new MessageBroker();
                                         Broker.Subscribe(Subscription);
                                     };

        private Because of = () =>
                                 {
                                     Broker.Publish(Sent);
                                     Handle.WaitOne(TimeSpan.FromSeconds(10));
                                 };

        private It should_match_the_dispatched_message = () => Received.ShouldBeTheSameAs(Sent);
    }
}