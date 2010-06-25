using Machine.Specifications;
using Rivet.Broker;

namespace Rivet.Specs
{
    public class when_sending_a_message_with_no_subscribers
    {
        private static IBroker Broker;
        private static object Message;        

        private Establish that = () =>
                                     {
                                         Broker = new MessageBroker();
                                         Message = new object();
                                     };

        private Because of = () => Broker.Send(Message);

        private It should_not_throw_an_exception = () => true.ShouldBeTrue();
    }
}