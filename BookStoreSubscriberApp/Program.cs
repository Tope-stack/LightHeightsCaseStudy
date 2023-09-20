// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    Port = 5672,
};
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("orders");


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
Console.ReadKey();
