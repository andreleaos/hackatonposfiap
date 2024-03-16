namespace hackatonposfiap.domain.Interfaces;
public interface IProducerRabbitMqService
{
    Task Publish(string message, Type objectType);
}
