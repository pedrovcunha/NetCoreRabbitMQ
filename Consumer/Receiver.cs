// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    const string queueName = "BasicTest";

    // we declare the queue here as well. Because we might start the consumer before the publisher,
    // we want to make sure the queue exists before we try to consume messages from it.
    channel.QueueDeclare(queueName, false, false, false, null);

    // We're about to tell the server to deliver us the messages from the queue. Since it will push us messages asynchronously,
    // we provide a callback. That is what EventingBasicConsumer.Received event handler does.
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received Message: {0}", message);        
    };

    channel.BasicConsume(queueName, true, consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}
