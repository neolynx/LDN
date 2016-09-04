namespace LDN.NodeA
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
                     var host = rmq.Host(new Uri("rabbitmq://127.0.0.1"), s =>
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
            var semaphore = new SemaphoreSlim(1);
            Console.CancelKeyPress += (_, __) =>
            {
                semaphore.Release();
            };
            semaphore.Wait();

            semaphore.Dispose();
            bus.Stop();
        }
    }
}
