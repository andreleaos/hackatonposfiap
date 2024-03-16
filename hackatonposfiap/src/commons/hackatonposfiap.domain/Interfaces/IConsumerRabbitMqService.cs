using hackatonposfiap.domain.Entities;

namespace hackatonposfiap.domain.Interfaces;
public interface IConsumerRabbitMqService
{
    Task<ObjectMessageProcessedInfo> Consume();
}
