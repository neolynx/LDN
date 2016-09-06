namespace LDN.NodeA
{
    using System;
    using System.Threading;
    using MassTransit;
    using Messages;

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(
                 rmq =>
                 {
                     var host = rmq.Host(new Uri("rabbitmq://localhost/ldn"), s =>
                     {
                         s.Password("ldn");
                         s.Username("ldn");
                     });
                     rmq.UseJsonSerializer();
                     rmq.ReceiveEndpoint(
                         host,
                         "a",
                         h =>
                         {
                             h.Consumer<MessageConsumer>();
                         });
                 });
            bus.Start();
            var semaphore = new ManualResetEventSlim(false);
            Console.CancelKeyPress += (_, __) =>
            {
                semaphore.Set();
            };

            bus.Publish(new DemoMessage { Content = 1 });
            semaphore.Wait();

            semaphore.Dispose();
            bus.Stop();
        }
    }
}
