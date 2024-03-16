using hackatonposfiap.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace hackatonposfiap.services;
public class ProducerRabbitMqService : IProducerRabbitMqService
{
    private readonly IModel _channel;
    private static string _exchangeName = null;
    private static string _routingKey = null;

    public ProducerRabbitMqService(IConfiguration configuration, IModel channel)
    {
        _channel = channel;
        _exchangeName = configuration["RabbitMqConfig:Exchange"];
        _routingKey = configuration["RabbitMqConfig:RoutingKey"];
    }

    public async Task Publish(string message, Type objectType)
    {
        object objDeserialized = JsonConvert.DeserializeObject(message, objectType);
        var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objDeserialized));

        string routingKey = _routingKey;

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: routingKey,
            false,
            basicProperties: null,
            body: messageBody
        );
    }
}
