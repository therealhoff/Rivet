using System.Threading;
using Machine.Specifications;
using Rivet.Broker;

namespace Rivet.Specs
{
    public class when_sending_a_message
    {
        private static Subscriber<object> Subscriber;
        private static object Message;
        private static object Received;
        private static IBroker Broker;
        private static ManualResetEvent Reset;

        private Establish that = () =>
                                     {
                                         Reset = new ManualResetEvent(false);
                                         Message = new object();
                                         Broker = new MessageBroker();
                                         Subscriber = (object msg, IReply reply) =>
                                                          {
                                                              Thread.Sleep(200);
                                                              Received = msg;
                                                              Reset.Set();
                                                          };
                                         Broker.Subscribe(Subscriber);
                                     };

        private Because of = () => Broker.Send(Message);

        private It should_have_received_the_same_message_as_was_sent = () =>
                                                                           {
                                                                               Reset.WaitOne();
                                                                               Received.ShouldBeTheSameAs(Message);
                                                                           };
    }
}