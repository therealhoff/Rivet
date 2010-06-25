using Machine.Specifications;
using Rivet.Broker;

namespace Rivet.Specs
{
    public class when_replying_to_a_message
    {
        private static IBroker Broker;
        private static object Message;
        private static Subscriber<object> Subscriber;
        private static string Reply;

        private Establish that = () =>
                                     {
                                         Reply = string.Empty;
                                         Broker = new MessageBroker();
                                         Message = new object();
                                         Subscriber = (object msg, IReply reply) =>
                                                          {
                                                              reply.Send("Hello");
                                                              reply.Send(" ");
                                                              reply.Send("World!");
                                                              reply.Send(new Complete());
                                                              reply.Send("do not process this message");
                                                          };
                                         Broker.Subscribe(Subscriber);
                                     };

        private Because of = () => Broker.Send(Message)
                                       .On((string val) => Reply += val)
                                       .WaitForReplies(3);

        private It should_receive_and_process_all_replies = () => Reply.ShouldEqual("Hello World!");
    }

    public class Complete
    {}
}