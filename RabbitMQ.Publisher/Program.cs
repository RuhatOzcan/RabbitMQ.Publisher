using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vzraghwb:N15hjuQFbV18uoVcMsUmYKBWth0dgnwG@cow.rmq2.cloudamqp.com/vzraghwb");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");
    Console.Write("Topic Belirtiniz : ");
    string topic = Console.ReadLine();
    channel.BasicPublish(
        exchange: "topic-exchange-example",
        routingKey: "topic",
        body: message
        );
}

Console.Read();