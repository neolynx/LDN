namespace LDN.NodeB
{
    using System;
    using System.Threading;
    using MassTransit;

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
                         "b",
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
            semaphore.Wait();

            semaphore.Dispose();
            bus.Stop();
        }
    }
}
