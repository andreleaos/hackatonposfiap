using hackatonposfiap.domain.Interfaces;

namespace hackatonposfiap.consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumerRabbitMqService _consumerRabbitMqService;

        public Worker(
            ILogger<Worker> logger,
            IConsumerRabbitMqService consumerRabbitMqService)
        {
            _logger = logger;
            _consumerRabbitMqService = consumerRabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("\nBackground Service em execucao no horario: {time}\n", DateTimeOffset.Now);
                await Process();
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task Process()
        {
            _logger.LogInformation("\nProcessando Consumer");
            await _consumerRabbitMqService.Consume();
            _logger.LogInformation("\nConsumer Processado");
        }
    }
}