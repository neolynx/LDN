namespace LDN.NodeB
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Messages;

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(
                  rmq =>
                  {
                      var host = rmq.Host(new Uri("rabbitmq://127.0.0.1/ldn"), s =>
                      {
                          s.Password("ldn");
                          s.Username("ldn");
                      });
                      rmq.UseJsonSerializer();
                      rmq.ReceiveEndpoint(
                          host,
                          "b",
                          h =>
                          {
                              h.Consumer<MessageConsumer>();
                          });
                  });
        }
    }

    internal class MessageConsumer : IConsumer<DemoMessage> {
        public Task Consume(ConsumeContext<DemoMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
