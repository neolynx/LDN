namespace LDN.NodeB
{
    using System;
    using MassTransit;

    class Program
    {
        static void Main(string[] args)
        {
            /* var bus =*/ Bus.Factory.CreateUsingRabbitMq(
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
}
