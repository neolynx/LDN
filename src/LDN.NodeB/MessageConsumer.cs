namespace LDN.NodeB
{
    using System.Threading.Tasks;
    using MassTransit;
    using Messages;

    internal class MessageConsumer : IConsumer<DemoMessage>
    {
        public Task Consume(ConsumeContext<DemoMessage> context)
        {
            var content = context.Message.Content + 1;
            var message = new DemoMessage { Content = content };
            return context.Publish(message);
        }
    }
}