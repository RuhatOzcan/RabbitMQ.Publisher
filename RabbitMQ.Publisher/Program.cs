﻿using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vzraghwb:N15hjuQFbV18uoVcMsUmYKBWth0dgnwG@cow.rmq2.cloudamqp.com/vzraghwb");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "example-work-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: message);

}

Console.Read();