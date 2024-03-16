using hackatonposfiap.domain.Interfaces;
using Newtonsoft.Json;

namespace hackatonposfiap.producer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProducerRabbitMqService _producerRabbitMqService;
        public Worker(
            ILogger<Worker> logger,
            IProducerRabbitMqService producerRabbitMqService)
        {
            _logger = logger;
            _producerRabbitMqService = producerRabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Process()
        {
        }
    }
}