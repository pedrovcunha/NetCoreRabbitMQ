// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    const string queueName = "BasicTest";

    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

    string message = "Lets Try RabbitMQ!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange:"", queueName, null, body);
    Console.WriteLine("[x] Sent message: {0}...", message);
}

Console.WriteLine("Press [Enter] to exit the Sender App...");
Console.ReadLine();

