namespace LDN.NodeB
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Messages;

    internal class MessageConsumer : IConsumer<DemoMessage>
    {
        public Task Consume(ConsumeContext<DemoMessage> context)
        {
            Console.WriteLine("Received content: " + context.Message.Content);
            if (context.Message.Content % 2 != 0)
            {
                var content = context.Message.Content + 1;
                var message = new DemoMessage { Content = content };
                return context.Publish(message);
            }
            return Task.CompletedTask;
        }
    }
}